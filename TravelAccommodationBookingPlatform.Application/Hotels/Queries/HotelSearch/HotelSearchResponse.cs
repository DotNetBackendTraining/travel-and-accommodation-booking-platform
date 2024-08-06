using TravelAccommodationBookingPlatform.Application.Shared.Responses;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch;

public class HotelSearchResponse
{
    public PageResponse<HotelSearchResult> SearchResults { get; set; } = default!;
    public AvailableFiltersResult? AvailableFilters { get; set; }

    public class HotelSearchResult
    {
        public HotelSummary HotelSummary { get; set; } = default!;
        public Price? MinimumPrice { get; set; }
        public Price? MaximumPrice { get; set; }
    }

    public class HotelSummary
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public StarRate StarRate { get; set; }
        public string Description { get; set; } = string.Empty;
        public Image ThumbnailImage { get; set; } = default!;
        public Guid CityId { get; set; }
        public string CityName { get; set; } = string.Empty;
    }

    public class AvailableFiltersResult
    {
        public Price MinimumPrice { get; set; } = default!;
        public Price MaximumPrice { get; set; } = default!;
        public IList<StarRate> AvailableStarRatings { get; set; } = default!;
        public IList<Amenity> AvailableAmenities { get; set; } = default!;
        public IList<RoomType> AvailableRoomTypes { get; set; } = default!;
    }
}