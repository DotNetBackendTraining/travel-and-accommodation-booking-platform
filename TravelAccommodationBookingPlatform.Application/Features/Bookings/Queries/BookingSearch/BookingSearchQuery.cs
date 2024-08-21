using TravelAccommodationBookingPlatform.Application.Features.Bookings.Queries.BookingSearch.DTOs;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Shared;

namespace TravelAccommodationBookingPlatform.Application.Features.Bookings.Queries.BookingSearch;

public class BookingSearchQuery : IQuery<BookingSearchResponse>
{
    public Guid UserId { get; set; }
    public PaginationParameters PaginationParameters { get; set; } = default!;
    public BookingSearchFilters Filters { get; set; } = new();
}