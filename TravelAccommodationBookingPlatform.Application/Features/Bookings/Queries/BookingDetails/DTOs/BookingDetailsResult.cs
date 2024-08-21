using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Features.Bookings.Queries.BookingDetails.DTOs;

public class BookingDetailsResult
{
    public Guid Id { get; set; }
    public Checking Checking { get; set; } = default!;
    public NumberOfGuests NumberOfGuests { get; set; } = default!;
    public SpecialRequest SpecialRequest { get; set; } = default!;
}