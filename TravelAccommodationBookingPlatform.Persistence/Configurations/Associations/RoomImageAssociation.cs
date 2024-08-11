using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.Configurations.Associations;

public class RoomImageAssociation : BaseEntity
{
    public Guid ImageId { get; set; }
    public Image Image { get; set; } = default!;
    public Guid RoomId { get; set; }
}