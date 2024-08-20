using Ardalis.Specification;
using AutoMapper;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Cities.Queries.CityDetails.Admin;

public sealed class AdminCityDetailsSpecification : Specification<City, AdminCityDetailsResponse>
{
    public AdminCityDetailsSpecification(Guid id, IMapper mapper)
    {
        Query.Select(c => new AdminCityDetailsResponse
            {
                CityDetails = mapper.Map<CityDetailsResponse>(c),
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                NumberOfHotels = c.Hotels.Count
            })
            .Include(c => c.ThumbnailImage)
            .Where(c => c.Id == id)
            .EnableCache(nameof(AdminCityDetailsSpecification), id);
    }
}