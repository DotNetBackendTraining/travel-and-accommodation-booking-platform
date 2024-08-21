using TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelSearch.Dtos;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Shared;

namespace TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelSearch;

public class HotelSearchQuery : IQuery<HotelSearchResponse>
{
    /// <summary>
    /// The parameters to control the pagination of the search results.
    /// </summary>
    public PaginationParameters PaginationParameters { get; set; } = default!;

    /// <summary>
    /// The filters to apply when searching for hotels.
    /// </summary>
    public HotelSearchFilters Filters { get; set; } = new();

    /// <summary>
    /// The options to control the behavior and additional details of the search query.
    /// </summary>
    public HotelSearchOptions Options { get; set; } = new();
}