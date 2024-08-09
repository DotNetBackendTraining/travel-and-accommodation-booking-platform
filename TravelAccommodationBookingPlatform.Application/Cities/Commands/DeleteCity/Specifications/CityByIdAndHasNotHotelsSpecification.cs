using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Cities.Commands.DeleteCity.Specifications;

public sealed class CityByIdAndHasNotHotelsSpecification : Specification<City>
{
    public CityByIdAndHasNotHotelsSpecification(Guid id)
    {
        Query.Where(c => c.Id == id)
            .Where(c => c.Hotels.Count == 0);
    }
}