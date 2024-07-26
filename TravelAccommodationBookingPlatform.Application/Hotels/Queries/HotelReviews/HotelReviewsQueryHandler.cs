using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelReviews;

public class HotelReviewsQueryHandler : IQueryHandler<HotelReviewsQuery, HotelReviewsResponse>
{
    private readonly IRepository<Hotel> _repository;

    public HotelReviewsQueryHandler(IRepository<Hotel> repository)
    {
        _repository = repository;
    }

    public async Task<Result<HotelReviewsResponse>> Handle(
        HotelReviewsQuery request,
        CancellationToken cancellationToken)
    {
        var specification = new HotelReviewsSpecification(request);
        var hotelReviewsResponse = await _repository.GetAsync(specification, cancellationToken);

        return hotelReviewsResponse is null
            ? Result.Failure<HotelReviewsResponse>(DomainErrors.Hotel.IdNotFound)
            : Result.Success(hotelReviewsResponse);
    }
}