using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Cities.Queries.CityDetails.Admin;

public class AdminCityDetailsQueryHandler : IQueryHandler<AdminCityDetailsQuery, AdminCityDetailsResponse>
{
    private readonly IRepository<City> _repository;
    private readonly IMapper _mapper;

    public AdminCityDetailsQueryHandler(IRepository<City> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<AdminCityDetailsResponse>> Handle(
        AdminCityDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new AdminCityDetailsSpecification(request.Id, _mapper);
        var adminCityDetailsResponse = await _repository.GetAsync(spec, cancellationToken);

        return adminCityDetailsResponse is null
            ? Result.Failure<AdminCityDetailsResponse>(DomainErrors.City.IdNotFound)
            : Result.Success(adminCityDetailsResponse);
    }
}