using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Domain.Entities;

public class Hotel : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public StarRate StarRate { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Owner { get; set; } = string.Empty;
    public Image ThumbnailImage { get; set; } = default!;
    public ICollection<Room> Rooms { get; set; } = default!;
    public Guid CityId { get; set; }
    public City City { get; set; } = default!;
    public Coordinates Coordinates { get; set; } = default!;
    public ICollection<Review> Reviews { get; set; } = default!;
    public ICollection<Image> Images { get; set; } = default!;
    public IList<Amenity> Amenities { get; set; } = default!;
    public ICollection<Discount> Discounts { get; set; } = default!;
}