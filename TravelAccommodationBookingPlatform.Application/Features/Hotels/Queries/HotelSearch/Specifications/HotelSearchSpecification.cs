using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelSearch.Dtos;
using TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelSearch.Specifications.Extensions;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelSearch.Specifications;

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