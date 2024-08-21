using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Features.Bookings.Queries.BookingSearch.DTOs;

public class BookingSearchResult
{
    public Guid Id { get; set; }
    public Checking Checking { get; set; } = default!;
    public BookingSearchHotelResult Hotel { get; set; } = default!;
}