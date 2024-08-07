using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.DTOs;

public class AvailableFiltersResult
{
    public Price MinimumPrice { get; set; } = default!;
    public Price MaximumPrice { get; set; } = default!;
    public IList<StarRate> AvailableStarRatings { get; set; } = default!;
    public IList<Amenity> AvailableAmenities { get; set; } = default!;
    public IList<RoomType> AvailableRoomTypes { get; set; } = default!;
}