using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.Specifications.Extensions;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.Specifications;

public sealed class HotelSearchSpecification : Specification<Hotel>
{
    public HotelSearchSpecification(HotelSearchQuery.HotelSearchFilters filters)
    {
        Query.ApplyHotelSearchFilters(filters)
            .OrderBy(h => h.Name);
    }
}