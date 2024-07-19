using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using TravelAccommodationBookingPlatform.Application.Interfaces.Authentication;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Application.Users.Commands.LoginUser;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Users.Commands;

public class LoginUserCommandHandlerTests
{
    [Theory, AutoMoqData]
    public async Task Handle_ReturnsFailure_WhenUserNotFound(
        [Frozen] Mock<IUserRepository> mockUserRepository,
        LoginUserCommandHandler handler,
        LoginUserCommand command)
    {
        mockUserRepository.Setup(repo => repo.GetUserByUsername(command.Username, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User)null!);

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(DomainErrors.User.UsernameNotFound);
    }

    [Theory, AutoMoqData]
    public async Task Handle_ReturnsFailure_WhenPasswordIsIncorrect(
        [Frozen] Mock<IUserRepository> mockUserRepository,
        [Frozen] Mock<IPasswordHashService> mockPasswordHashService,
        User user,
        LoginUserCommand command,
        LoginUserCommandHandler handler)
    {
        command.Username = user.Username;
        mockUserRepository.Setup(repo => repo.GetUserByUsername(command.Username, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);
        mockPasswordHashService.Setup(service => service.VerifyPassword(command.Password, user.PasswordHash))
            .Returns(false);

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(DomainErrors.User.InvalidCredentials);
    }

    [Theory, AutoMoqData]
    public async Task Handle_ReturnsSuccess_WithToken_WhenCredentialsAreValid(
        [Frozen] Mock<IUserRepository> mockUserRepository,
        [Frozen] Mock<IPasswordHashService> mockPasswordHashService,
        [Frozen] Mock<IJwtTokenGenerator> mockJwtTokenGenerator,
        User user,
        string token,
        string password,
        string passwordHash,
        LoginUserCommand command,
        LoginUserCommandHandler handler)
    {
        command.Username = user.Username;
        command.Password = password;
        user.PasswordHash = passwordHash;
        mockUserRepository.Setup(repo => repo.GetUserByUsername(command.Username, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);
        mockPasswordHashService.Setup(service => service.VerifyPassword(command.Password, user.PasswordHash))
            .Returns(true);
        mockJwtTokenGenerator.Setup(generator => generator.GenerateToken(user))
            .Returns(token);

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Token.Should().Be(token);
    }
}