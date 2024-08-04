using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Cities.Queries.CitySearch;

public class CitySearchQueryHandler : IQueryHandler<CitySearchQuery, CitySearchResponse>
{
    private readonly IRepository<City> _repository;
    private readonly IMapper _mapper;

    public CitySearchQueryHandler(
        IRepository<City> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<CitySearchResponse>> Handle(
        CitySearchQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new CitySearchSpecification(_mapper);
        var page = await _repository.PageAsync(spec, request.PaginationParameters, cancellationToken);
        return Result.Success(new CitySearchResponse { Results = page });
    }
}