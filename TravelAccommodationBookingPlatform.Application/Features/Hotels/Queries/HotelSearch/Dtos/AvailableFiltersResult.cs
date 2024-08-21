using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelSearch.Dtos;

public class AvailableFiltersResult
{
    public Price MinimumPrice { get; set; } = default!;
    public Price MaximumPrice { get; set; } = default!;
    public IList<StarRate> AvailableStarRatings { get; set; } = default!;
    public IList<Amenity> AvailableAmenities { get; set; } = default!;
    public IList<RoomType> AvailableRoomTypes { get; set; } = default!;
}