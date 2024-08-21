using Microsoft.AspNetCore.Http;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;
using TravelAccommodationBookingPlatform.Presentation.Attributes;

namespace TravelAccommodationBookingPlatform.Presentation.Features.Cities.Requests;

public class CreateCityRequest
{
    /// <summary>
    /// The name of the city to be created.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The country where the city is located.
    /// </summary>
    public Country Country { get; set; } = default!;

    /// <summary>
    /// The post office details for the city.
    /// </summary>
    public PostOffice PostOffice { get; set; } = default!;

    /// <summary>
    /// The thumbnail image representing the city.
    /// </summary>
    [ValidImageExtensions]
    public IFormFile ThumbnailImage { get; set; } = default!;
}