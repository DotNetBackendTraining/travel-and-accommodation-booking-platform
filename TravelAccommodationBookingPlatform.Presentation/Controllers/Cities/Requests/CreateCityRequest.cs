using Microsoft.AspNetCore.Http;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;
using TravelAccommodationBookingPlatform.Presentation.Attributes;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers.Cities.Requests;

public class CreateCityRequest
{
    public string Name { get; set; } = string.Empty;
    public Country Country { get; set; } = default!;
    public PostOffice PostOffice { get; set; } = default!;
    [ValidImageExtensions] public IFormFile ThumbnailImage { get; set; } = default!;
}