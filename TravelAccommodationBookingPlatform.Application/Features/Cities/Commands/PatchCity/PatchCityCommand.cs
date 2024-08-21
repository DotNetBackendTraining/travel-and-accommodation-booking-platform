using TravelAccommodationBookingPlatform.Application.Interfaces.IO;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;

namespace TravelAccommodationBookingPlatform.Application.Features.Cities.Commands.PatchCity;

public class PatchCityCommand : ICommand
{
    public Guid Id { get; set; }
    public IPatchDocument<PatchCityModel> PatchDocument { get; set; } = default!;
}