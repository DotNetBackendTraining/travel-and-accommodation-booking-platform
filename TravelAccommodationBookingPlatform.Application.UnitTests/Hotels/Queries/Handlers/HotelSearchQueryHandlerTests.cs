using AutoFixture.Xunit2;
using Moq;
using TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelSearch;
using TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelSearch.Specifications;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Hotels.Queries.Handlers;

public class HotelSearchQueryHandlerTests
{
    [Theory, AutoMoqData]
    public async Task Handle_CallsPageAsync_WithCorrectParameters(
        HotelSearchQuery query,
        [Frozen] Mock<IRepository<Hotel>> mockRepository,
        HotelSearchQueryHandler handler)
    {
        // Arrange
        query.Options.IncludeAvailableSearchFilters = false;
        query.PaginationParameters.PageNumber = 1;
        query.PaginationParameters.PageSize = 10;

        // Act
        await handler.Handle(query, CancellationToken.None);

        // Assert
        mockRepository.Verify(r => r.PageAsync(
                It.IsAny<HotelSearchResultSpecification>(),
                query.PaginationParameters,
                It.IsAny<CancellationToken>()),
            Times.Once);
        mockRepository.Verify(r => r.AggregateAsync(
                It.IsAny<HotelSearchSpecification>(),
                It.IsAny<AvailableFiltersResultSpecification>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Theory, AutoMoqData]
    public async Task Handle_CallsAggregateAsync_WhenIncludeSearchResultFiltersIsTrue(
        HotelSearchQuery query,
        [Frozen] Mock<IRepository<Hotel>> mockRepository,
        HotelSearchQueryHandler handler)
    {
        // Arrange
        query.Options.IncludeAvailableSearchFilters = true;
        query.PaginationParameters.PageNumber = 1;
        query.PaginationParameters.PageSize = 10;

        // Act
        await handler.Handle(query, CancellationToken.None);

        // Assert
        mockRepository.Verify(r => r.PageAsync(
                It.IsAny<HotelSearchResultSpecification>(),
                query.PaginationParameters,
                It.IsAny<CancellationToken>()),
            Times.Once);
        mockRepository.Verify(r => r.AggregateAsync(
                It.IsAny<HotelSearchSpecification>(),
                It.IsAny<AvailableFiltersResultSpecification>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}