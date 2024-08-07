using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.DTOs;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.Specifications.Extensions;

public static class HotelSearchOptionsSpecificationExtensions
{
    public static ISpecificationBuilder<Hotel> ApplyHotelSortingOption(
        this ISpecificationBuilder<Hotel> query,
        HotelSearchOptions.SortingOption sortingOption)
    {
        switch (sortingOption)
        {
            case HotelSearchOptions.SortingOption.Name:
                query.OrderBy(h => h.Name);
                break;

            case HotelSearchOptions.SortingOption.Featured:
                // Feature score is proportional to the discount rate
                query.Where(h => h.ActiveDiscount != null)
                    .OrderByDescending(h => h.ActiveDiscount!.Rate.Percentage);
                break;

            case HotelSearchOptions.SortingOption.StarsThenName:
                query.OrderByDescending(h => h.StarRate)
                    .ThenBy(h => h.Name);
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(sortingOption), sortingOption, null);
        }

        return query;
    }
}