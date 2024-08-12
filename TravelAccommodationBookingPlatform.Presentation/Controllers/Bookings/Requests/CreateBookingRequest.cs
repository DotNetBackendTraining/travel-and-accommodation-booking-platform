using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers.Bookings.Requests;

public class CreateBookingRequest
{
    public List<Guid> RoomIds { get; set; } = default!;
    public Checking Checking { get; set; } = default!;
    public NumberOfGuests NumberOfGuests { get; set; } = default!;
    public SpecialRequest SpecialRequest { get; set; } = default!;
}