using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Features.Cities.Commands.DeleteCity.Specifications;

public sealed class CityByIdIncludingImagesSpecification : Specification<City>
{
    public CityByIdIncludingImagesSpecification(Guid id)
    {
        Query.Where(c => c.Id == id)
            .Include(c => c.ThumbnailImage);
    }
}