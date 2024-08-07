using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.Specifications.Extensions;

public static class HotelSearchOptionsSpecificationExtensions
{
    public static ISpecificationBuilder<Hotel> ApplyHotelSortingOption(
        this ISpecificationBuilder<Hotel> query,
        HotelSearchQuery.SortingOption sortingOption)
    {
        switch (sortingOption)
        {
            case HotelSearchQuery.SortingOption.Name:
                query.OrderBy(h => h.Name);
                break;

            case HotelSearchQuery.SortingOption.Featured:
                // Feature score is proportional to the discount rate
                query.Where(h => h.ActiveDiscount != null)
                    .OrderByDescending(h => h.ActiveDiscount!.Rate.Percentage);
                break;

            case HotelSearchQuery.SortingOption.StarsThenName:
                query.OrderByDescending(h => h.StarRate)
                    .ThenBy(h => h.Name);
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(sortingOption), sortingOption, null);
        }

        return query;
    }
}