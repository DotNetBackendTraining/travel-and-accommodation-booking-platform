using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Bookings.Commands.CreateBooking;

public class CreateBookingCommand : ICommand<CreateBookingResponse>
{
    public Guid UserId { get; set; }
    public List<Guid> RoomIds { get; set; } = default!;
    public Checking Checking { get; set; } = default!;
    public NumberOfGuests NumberOfGuests { get; set; } = default!;
    public SpecialRequest SpecialRequest { get; set; } = default!;
}