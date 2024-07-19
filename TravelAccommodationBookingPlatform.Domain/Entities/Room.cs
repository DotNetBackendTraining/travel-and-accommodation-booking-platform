using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Domain.Entities;

public class Room : BaseEntity
{
    public Guid HotelId { get; set; }
    public Hotel Hotel { get; set; } = default!;
    public int RoomNumber { get; set; }
    public RoomType RoomType { get; set; }
    public string Description { get; set; } = string.Empty;
    public Price Price { get; set; } = default!;
    public ICollection<Booking> Bookings { get; set; } = default!;
    public NumberOfGuests MaxNumberOfGuests { get; set; } = default!;
    public ICollection<Image> Images { get; set; } = default!;
}