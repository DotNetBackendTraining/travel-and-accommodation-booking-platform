using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.DTOs;
using TravelAccommodationBookingPlatform.Application.Shared.Responses;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch;

public class HotelSearchResponse
{
    public PageResponse<HotelSearchResult> SearchResults { get; set; } = default!;
    public AvailableFiltersResult? AvailableFilters { get; set; }
}