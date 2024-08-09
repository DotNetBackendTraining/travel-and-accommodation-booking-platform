using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Cities.Commands.PatchCity;

public class PatchCityModel
{
    public string? Name { get; set; }
    public Country? Country { get; set; }
    public PostOffice? PostOffice { get; set; }
}