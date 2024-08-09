using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Commands.PatchHotel;

public class PatchHotelModel
{
    public string? Name { get; set; }
    public StarRate? StarRate { get; set; }
    public string? Description { get; set; }
    public string? Owner { get; set; }
    public Coordinates? Coordinates { get; set; }
    public IList<Amenity>? Amenities { get; set; }
    public Guid? CityId { get; set; }
}