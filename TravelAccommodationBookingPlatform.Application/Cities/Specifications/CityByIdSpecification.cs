using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Cities.Specifications;

public sealed class CityByIdSpecification : Specification<City>
{
    public CityByIdSpecification(Guid id)
    {
        Query.Where(c => c.Id == id);
    }
}