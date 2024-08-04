using TravelAccommodationBookingPlatform.Application.Cities.Specifications;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Cities.Queries.CityDetails;

public class CityDetailsQueryHandler : IQueryHandler<CityDetailsQuery, CityDetailsResponse>
{
    private readonly IRepository<City> _repository;

    public CityDetailsQueryHandler(IRepository<City> repository)
    {
        _repository = repository;
    }

    public async Task<Result<CityDetailsResponse>> Handle(CityDetailsQuery request, CancellationToken cancellationToken)
    {
        var spec = new CityByIdSpecification(request.Id);
        var cityDetailsResponse = await _repository
            .GetWithProjectionAsync<CityDetailsResponse>(spec, cancellationToken);

        return cityDetailsResponse is null
            ? Result.Failure<CityDetailsResponse>(DomainErrors.City.IdNotFound)
            : Result.Success(cityDetailsResponse);
    }
}