using Microsoft.AspNetCore.Http;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;
using TravelAccommodationBookingPlatform.Presentation.Attributes;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers.Hotels.Requests;

public class CreateHotelRequest
{
    /// <summary>
    /// The name of the hotel.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The star rating of the hotel, represented by the <see cref="StarRate"/> enum.
    /// </summary>
    public StarRate StarRate { get; set; }

    /// <summary>
    /// A description of the hotel, detailing its features and offerings.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The name of the owner of the hotel.
    /// </summary>
    public string Owner { get; set; } = string.Empty;

    /// <summary>
    /// The thumbnail image representing the hotel.
    /// </summary>
    [ValidImageExtensions]
    public IFormFile ThumbnailImage { get; set; } = default!;

    /// <summary>
    /// A collection of additional images of the hotel.
    /// </summary>
    [ValidImageExtensions]
    public IEnumerable<IFormFile>? Images { get; set; } = [];

    /// <summary>
    /// The ID of the city where the hotel is located.
    /// </summary>
    public Guid CityId { get; set; }

    /// <summary>
    /// The coordinates (latitude and longitude) of the hotel's location.
    /// </summary>
    public Coordinates Coordinates { get; set; } = default!;

    /// <summary>
    /// A list of amenities available at the hotel.
    /// </summary>
    public IList<Amenity>? Amenities { get; set; } = [];
}