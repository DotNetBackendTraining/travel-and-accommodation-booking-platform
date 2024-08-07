using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.Specifications;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch;

public class HotelSearchQueryHandler : IQueryHandler<HotelSearchQuery, HotelSearchResponse>
{
    private readonly IRepository<Hotel> _repository;
    private readonly IMapper _mapper;

    public HotelSearchQueryHandler(
        IRepository<Hotel> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<HotelSearchResponse>> Handle(HotelSearchQuery request, CancellationToken cancellationToken)
    {
        var resultsSpec = new HotelSearchResultsSpecification(
            request.Filters,
            request.Options,
            _mapper);

        var resultsPage = await _repository.PageAsync(resultsSpec, request.PaginationParameters, cancellationToken);
        var response = new HotelSearchResponse { SearchResults = resultsPage };

        if (request.Options.IncludeAvailableSearchFilters)
        {
            var hotelsSpec = new HotelSearchSpecification(request.Filters, request.Options.SortingOption);
            var filtersSpec = new AvailableFiltersSpecification();
            response.AvailableFilters = await _repository.AggregateAsync(hotelsSpec, filtersSpec, cancellationToken);
        }

        return Result.Success(response);
    }
}