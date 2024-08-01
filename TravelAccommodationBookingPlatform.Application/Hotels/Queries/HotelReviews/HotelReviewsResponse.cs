using TravelAccommodationBookingPlatform.Application.Shared.Pagination;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelReviews;

public class HotelReviewsResponse
{
    public Guid Id { get; set; }
    public required PageResponse<Review> Results { get; set; }
}