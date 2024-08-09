using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Commands.DeleteHotel;

public class DeleteHotelCommand : ICommand
{
    public Guid Id { get; set; }
}