using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;

namespace TravelAccommodationBookingPlatform.Application.Features.Hotels.Commands.DeleteHotel;

public class DeleteHotelCommand : ICommand
{
    public Guid Id { get; set; }
}