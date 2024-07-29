using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Domain.Entities;

public class Booking : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    public Checking Checking { get; set; } = default!;
    public ICollection<Room> Rooms { get; set; } = default!;
    public NumberOfGuests NumberOfGuests { get; set; } = default!;
    public SpecialRequest? SpecialRequest { get; set; }
    public Guid? PaymentId { get; set; }
    public Payment? Payment { get; set; }
}