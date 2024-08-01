using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Domain.Entities;

public class Discount : BaseEntity
{
    public Guid HotelId { get; set; }
    public Hotel Hotel { get; set; } = default!;
    public DiscountRate Rate { get; set; } = default!;
}