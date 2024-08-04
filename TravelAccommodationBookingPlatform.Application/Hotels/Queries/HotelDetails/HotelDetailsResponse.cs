using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelDetails;

public class HotelDetailsResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public StarRate StarRate { get; set; }
    public string Description { get; set; } = string.Empty;
    public Image ThumbnailImage { get; set; } = default!;
    public Guid CityId { get; set; }
    public Coordinates Coordinates { get; set; } = default!;
    public IList<Amenity> Amenities { get; set; } = default!;
}