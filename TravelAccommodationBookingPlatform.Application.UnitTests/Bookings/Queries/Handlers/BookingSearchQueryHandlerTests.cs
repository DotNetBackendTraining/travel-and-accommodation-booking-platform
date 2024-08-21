using AutoFixture.Xunit2;
using Moq;
using TravelAccommodationBookingPlatform.Application.Features.Bookings.Queries.BookingSearch;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Bookings.Queries.Handlers;

public class BookingSearchQueryHandlerTests
{
    [Theory, AutoMoqData]
    public async Task Handle_CallsPageAsync_WithCorrectParameters(
        BookingSearchQuery query,
        [Frozen] Mock<IRepository<Booking>> mockRepository,
        BookingSearchQueryHandler handler)
    {
        // Arrange
        query.PaginationParameters.PageNumber = 1;
        query.PaginationParameters.PageSize = 10;

        // Act
        await handler.Handle(query, CancellationToken.None);

        // Assert
        mockRepository.Verify(r => r.PageAsync(
                It.IsAny<BookingSearchSpecification>(),
                query.PaginationParameters,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}