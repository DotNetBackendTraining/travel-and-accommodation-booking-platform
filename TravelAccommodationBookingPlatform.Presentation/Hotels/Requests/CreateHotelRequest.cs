using Microsoft.AspNetCore.Http;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;
using TravelAccommodationBookingPlatform.Presentation.Attributes;

namespace TravelAccommodationBookingPlatform.Presentation.Hotels.Requests;

public class CreateHotelRequest
{
    public string Name { get; set; } = string.Empty;
    public StarRate StarRate { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Owner { get; set; } = string.Empty;
    [ValidImageExtensions] public IFormFile ThumbnailImage { get; set; } = default!;
    [ValidImageExtensions] public IEnumerable<IFormFile>? Images { get; set; } = [];
    public Guid CityId { get; set; }
    public Coordinates Coordinates { get; set; } = default!;
    public IList<Amenity>? Amenities { get; set; } = [];
}