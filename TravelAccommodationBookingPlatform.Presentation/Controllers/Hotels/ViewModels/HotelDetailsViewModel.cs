using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers.Hotels.ViewModels;

public class HotelDetailsViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public StarRate StarRate { get; set; }
    public string Description { get; set; } = string.Empty;
    public Image ThumbnailImage { get; set; } = default!;
    public Guid CityId { get; set; }
    public string CityName { get; set; } = string.Empty;
    public Coordinates Coordinates { get; set; } = default!;
    public IList<Amenity> Amenities { get; set; } = default!;
}