using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.FeaturedHotels;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Application.Shared.Responses;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Hotels.Queries.Handlers;

public class FeaturedHotelsQueryHandlerTests
{
    [Theory, AutoMoqData(omitOnRecursion: true)]
    public async Task Handle_ReturnsCorrectResponse(
        [Frozen] Mock<IRepository<Hotel>> repositoryMock,
        FeaturedHotelsQueryHandler handler,
        FeaturedHotelsQuery query,
        PageResponse<FeaturedHotelsResponse.FeaturedDealResult> pageResponse)
    {
        // Arrange
        repositoryMock.Setup(r => r.PageAsync(
                It.IsAny<FeaturedHotelsSpecification>(),
                query.PaginationParameters,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(pageResponse);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.Results.Should().BeEquivalentTo(pageResponse);

        repositoryMock.Verify(r => r.PageAsync(
                It.IsAny<FeaturedHotelsSpecification>(),
                query.PaginationParameters,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}