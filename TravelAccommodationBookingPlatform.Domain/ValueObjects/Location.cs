using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Domain.ValueObjects;

public class Location
{
    public City City { get; set; } = default!;
    public Coordinates Coordinates { get; set; } = default!;
}