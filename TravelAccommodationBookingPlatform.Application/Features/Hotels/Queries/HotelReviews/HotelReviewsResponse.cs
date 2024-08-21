using TravelAccommodationBookingPlatform.Application.Shared.Responses;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelReviews;

public class HotelReviewsResponse
{
    public Guid Id { get; set; }
    public required PageResponse<Review> Results { get; set; }
}