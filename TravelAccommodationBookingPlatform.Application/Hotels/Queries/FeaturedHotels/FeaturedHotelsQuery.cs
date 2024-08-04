using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Shared.Pagination;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.FeaturedHotels;

public class FeaturedHotelsQuery : IQuery<FeaturedHotelsResponse>
{
    public PaginationParameters PaginationParameters { get; set; } = new()
    {
        PageNumber = 1,
        PageSize = 5
    };
}