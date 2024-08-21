using TravelAccommodationBookingPlatform.Application.Interfaces.IO;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Features.Cities.Commands.CreateCity;

public class CreateCityCommand : ICommand<CreateCityResponse>
{
    public string Name { get; set; } = string.Empty;
    public Country Country { get; set; } = default!;
    public PostOffice PostOffice { get; set; } = default!;
    public IFile ThumbnailImage { get; set; } = default!;
}