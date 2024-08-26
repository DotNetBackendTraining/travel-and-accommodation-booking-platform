using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;

namespace TravelAccommodationBookingPlatform.Application.Features.Bookings.Commands.DeleteBooking;

public class DeleteBookingCommand : ICommand
{
    public Guid UserId { get; set; }
    public Guid BookingId { get; set; }
}