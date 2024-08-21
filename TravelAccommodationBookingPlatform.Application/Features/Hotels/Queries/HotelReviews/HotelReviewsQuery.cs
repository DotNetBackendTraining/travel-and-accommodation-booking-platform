using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Shared;

namespace TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelReviews;

public class HotelReviewsQuery : IQuery<HotelReviewsResponse>
{
    public Guid Id { get; set; }
    public PaginationParameters PaginationParameters { get; set; } = default!;
}