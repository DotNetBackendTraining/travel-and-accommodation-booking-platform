using Ardalis.Specification;
using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Features.Cities.Queries.CitySearch.SpecificationExtensions;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Features.Cities.Queries.CitySearch;

public sealed class CitySearchSpecification : Specification<City, CitySearchResponse.CitySearchResult>
{
    public CitySearchSpecification(IMapper mapper)
    {
        Query.Select(c => mapper.Map<CitySearchResponse.CitySearchResult>(c))
            .SortByVisitsDescending()
            .ThenBy(c => c.Name)
            .Include(c => c.ThumbnailImage);
    }
}