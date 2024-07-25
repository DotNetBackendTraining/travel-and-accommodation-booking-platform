using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Shared.Pagination;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelReviews;

public class HotelReviewsQuery : IQuery<HotelReviewsResponse>
{
    public Guid Id { get; set; }
    public PaginationParameters PaginationParameters { get; set; } = default!;
}