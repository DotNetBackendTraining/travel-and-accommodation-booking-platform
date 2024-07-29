using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Domain.Entities;

public class City : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public Country Country { get; set; } = default!;
    public PostOffice PostOffice { get; set; } = default!;
    public Image ThumbnailImage { get; set; } = default!;
    public ICollection<Hotel> Hotels { get; set; } = default!;
}