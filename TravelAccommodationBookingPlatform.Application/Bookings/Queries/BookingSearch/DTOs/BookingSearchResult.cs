using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingSearch.DTOs;

public class BookingSearchResult
{
    public Checking Checking { get; set; } = default!;
    public BookingSearchHotelResult Hotel { get; set; } = default!;
}