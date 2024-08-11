using TravelAccommodationBookingPlatform.Application.Shared.Responses;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Cities.Queries.CitySearch;

public class CitySearchResponse
{
    public required PageResponse<CitySearchResult> Results { get; set; }

    public class CitySearchResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Country Country { get; set; } = default!;
        public PostOffice PostOffice { get; set; } = default!;
        public Image ThumbnailImage { get; set; } = default!;
    }
}