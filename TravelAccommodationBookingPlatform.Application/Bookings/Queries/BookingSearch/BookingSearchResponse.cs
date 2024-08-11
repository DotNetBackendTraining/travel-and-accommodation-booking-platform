using TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingSearch.DTOs;
using TravelAccommodationBookingPlatform.Application.Shared.Responses;

namespace TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingSearch;

public class BookingSearchResponse
{
    public PageResponse<BookingSearchResult> Results { get; set; } = default!;
}