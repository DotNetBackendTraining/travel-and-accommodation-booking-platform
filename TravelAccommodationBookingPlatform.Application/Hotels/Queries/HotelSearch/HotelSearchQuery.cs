using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.DTOs;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Shared;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch;

public class HotelSearchQuery : IQuery<HotelSearchResponse>
{
    public PaginationParameters PaginationParameters { get; set; } = default!;
    public HotelSearchFilters Filters { get; set; } = new();
    public HotelSearchOptions Options { get; set; } = new();
}