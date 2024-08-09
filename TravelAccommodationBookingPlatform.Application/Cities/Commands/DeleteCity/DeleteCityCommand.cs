using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;

namespace TravelAccommodationBookingPlatform.Application.Cities.Commands.DeleteCity;

public class DeleteCityCommand : ICommand
{
    public Guid Id { get; set; }
}