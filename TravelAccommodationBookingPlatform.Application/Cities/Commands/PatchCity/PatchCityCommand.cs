using TravelAccommodationBookingPlatform.Application.Interfaces.Files;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;

namespace TravelAccommodationBookingPlatform.Application.Cities.Commands.PatchCity;

public class PatchCityCommand : ICommand
{
    public Guid Id { get; set; }
    public IPatchDocument<PatchCityModel> PatchDocument { get; set; } = default!;
}