using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelReviews;

public sealed class HotelReviewsSpecification : Specification<Hotel, HotelReviewsResponse>
{
    public HotelReviewsSpecification(HotelReviewsQuery query)
    {
        Query.Select(h => new HotelReviewsResponse
        {
            Id = h.Id,
            Items = h.Reviews
                .AsQueryable()
                .OrderBy(i => i.Text)
                .Skip((query.PaginationParameters.PageNumber - 1) * query.PaginationParameters.PageSize)
                .Take(query.PaginationParameters.PageSize)
                .ToList(),
            TotalCount = h.Reviews.Count
        }).Where(h => h.Id == query.Id);
    }
}