using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;

namespace TravelAccommodationBookingPlatform.Application.Features.Cities.Commands.DeleteCity;

public class DeleteCityCommand : ICommand
{
    public Guid Id { get; set; }
}