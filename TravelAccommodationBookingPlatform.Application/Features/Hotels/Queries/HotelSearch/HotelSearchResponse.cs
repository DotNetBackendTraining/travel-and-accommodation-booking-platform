using TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelSearch.Dtos;
using TravelAccommodationBookingPlatform.Application.Shared.Responses;

namespace TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelSearch;

public class HotelSearchResponse
{
    public PageResponse<HotelSearchResult> SearchResults { get; set; } = default!;
    public AvailableFiltersResult? AvailableFilters { get; set; }
}