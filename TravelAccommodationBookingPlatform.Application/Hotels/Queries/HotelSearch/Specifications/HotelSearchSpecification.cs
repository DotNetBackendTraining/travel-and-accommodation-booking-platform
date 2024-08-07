using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.DTOs;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.Specifications.Extensions;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.Specifications;

public sealed class HotelSearchSpecification : Specification<Hotel>
{
    public HotelSearchSpecification(
        HotelSearchFilters filters,
        HotelSearchOptions.SortingOption sortingOption)
    {
        Query.ApplyHotelSearchFilters(filters)
            .ApplyHotelSortingOption(sortingOption);
    }
}