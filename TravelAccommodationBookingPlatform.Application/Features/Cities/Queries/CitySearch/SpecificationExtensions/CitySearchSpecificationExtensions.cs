using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Features.Cities.Queries.CitySearch.SpecificationExtensions;

public static class CitySearchSpecificationExtensions
{
    public static IOrderedSpecificationBuilder<City> SortByVisitsDescending(
        this ISpecificationBuilder<City> query)
    {
        return query
            .OrderByDescending(c => c.Hotels
                .SelectMany(h => h.Rooms)
                .SelectMany(r => r.Bookings)
                .Distinct()
                .Count());
    }
}