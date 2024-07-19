using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Infrastructure.Services.Authentication;
using TravelAccommodationBookingPlatform.Infrastructure.Settings;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Infrastructure.UnitTests.Services.Authentication;

public class JwtTokenGeneratorTests
{
    [Theory, AutoMoqData]
    public void GenerateToken_ReturnsValidToken_WithCorrectClaims(
        [Frozen] Mock<IOptions<JwtSettings>> mockOptions,
        [Frozen] JwtSettings settings,
        User user,
        Fixture fixture)
    {
        mockOptions.Setup(o => o.Value).Returns(settings);
        var tokenGenerator = fixture.Create<JwtTokenGenerator>();

        var token = tokenGenerator.GenerateToken(user);
        var handler = new JwtSecurityTokenHandler();
        var decodedToken = handler.ReadJwtToken(token);

        decodedToken.Claims.First(c => c.Type == "sub").Value.Should().Be(user.Username);
        decodedToken.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value.Should().Be(user.Id.ToString());
        decodedToken.Claims.First(c => c.Type == ClaimTypes.Role).Value.Should().Be(user.UserRole.ToString());
        decodedToken.Issuer.Should().Be(settings.Issuer);
        decodedToken.Audiences.First().Should().Be(settings.Audience);
        decodedToken.ValidTo.Should().BeCloseTo(DateTime.UtcNow.AddHours(settings.ExpiryHours),
            precision: TimeSpan.FromMinutes(1));
    }
}