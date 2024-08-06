using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Shared;

namespace TravelAccommodationBookingPlatform.Application.Cities.Queries.CitySearch;

public class CitySearchQuery : IQuery<CitySearchResponse>
{
    public PaginationParameters PaginationParameters { get; set; } = default!;
}