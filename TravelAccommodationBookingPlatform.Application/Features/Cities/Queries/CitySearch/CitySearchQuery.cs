using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Shared;

namespace TravelAccommodationBookingPlatform.Application.Features.Cities.Queries.CitySearch;

public class CitySearchQuery : IQuery<CitySearchResponse>
{
    /// <summary>
    /// The parameters to control the pagination of the search results.
    /// </summary>
    public PaginationParameters PaginationParameters { get; set; } = default!;
}