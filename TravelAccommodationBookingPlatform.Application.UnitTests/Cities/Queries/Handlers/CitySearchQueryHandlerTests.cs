using AutoFixture.Xunit2;
using Moq;
using TravelAccommodationBookingPlatform.Application.Features.Cities.Queries.CitySearch;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Cities.Queries.Handlers;

public class CitySearchQueryHandlerTests
{
    [Theory, AutoMoqData]
    public async Task Handle_CallsPageAsync_WithCorrectParameters(
        CitySearchQuery query,
        [Frozen] Mock<IRepository<City>> mockRepository,
        CitySearchQueryHandler handler)
    {
        // Arrange
        query.PaginationParameters.PageNumber = 1;
        query.PaginationParameters.PageSize = 10;

        // Act
        await handler.Handle(query, CancellationToken.None);

        // Assert
        mockRepository.Verify(r => r.PageAsync(
                It.IsAny<CitySearchSpecification>(),
                query.PaginationParameters,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}