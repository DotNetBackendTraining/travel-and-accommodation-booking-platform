using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Application.Shared;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelDetails;

public class HotelDetailsQueryHandler : IQueryHandler<HotelDetailsQuery, HotelDetailsResponse>
{
    private readonly IGenericRepository<Hotel> _genericRepository;

    public HotelDetailsQueryHandler(IGenericRepository<Hotel> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<Result<HotelDetailsResponse>> Handle(
        HotelDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var specification = new Specification<Hotel>.Builder()
            .SetCriteria(h => h.Id == request.Id)
            .Build();

        var hotelDetailsResponse = await _genericRepository
            .GetFirstBySpecificationAsync<HotelDetailsResponse>(specification, cancellationToken);

        return hotelDetailsResponse is null
            ? Result.Failure<HotelDetailsResponse>(DomainErrors.Hotel.IdNotFound)
            : Result.Success(hotelDetailsResponse);
    }
}