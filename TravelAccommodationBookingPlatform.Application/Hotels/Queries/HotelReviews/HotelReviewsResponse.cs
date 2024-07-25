using TravelAccommodationBookingPlatform.Application.Shared.Pagination;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelReviews;

public class HotelReviewsResponse : PagedCollectionResponse<Review>
{
    public Guid Id { get; set; }
}