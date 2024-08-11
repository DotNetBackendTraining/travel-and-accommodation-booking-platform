using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.Configurations.Associations;

public class HotelImageAssociation : BaseEntity
{
    public Guid ImageId { get; set; }
    public Image Image { get; set; } = default!;
    public Guid HotelId { get; set; }
}