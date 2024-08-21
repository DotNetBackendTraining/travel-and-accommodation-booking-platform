using TravelAccommodationBookingPlatform.Application.Features.Hotels.Specifications;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelDetails;

public class HotelDetailsQueryHandler : IQueryHandler<HotelDetailsQuery, HotelDetailsResponse>
{
    private readonly IRepository<Hotel> _repository;

    public HotelDetailsQueryHandler(IRepository<Hotel> repository)
    {
        _repository = repository;
    }

    public async Task<Result<HotelDetailsResponse>> Handle(
        HotelDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new HotelByIdSpecification(request.Id);
        var hotelDetailsResponse = await _repository
            .GetWithProjectionAsync<HotelDetailsResponse>(spec, cancellationToken);

        return hotelDetailsResponse is null
            ? Result.Failure<HotelDetailsResponse>(DomainErrors.Hotel.IdNotFound)
            : Result.Success(hotelDetailsResponse);
    }
}