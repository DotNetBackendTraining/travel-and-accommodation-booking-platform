using Ardalis.Specification;
using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.DTOs;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.Specifications.Extensions;
using TravelAccommodationBookingPlatform.Application.Shared.Responses;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.Specifications;

public sealed class HotelSearchResultSpecification : Specification<Hotel, HotelSearchResult>
{
    public HotelSearchResultSpecification(
        HotelSearchFilters filters,
        HotelSearchOptions options,
        IMapper mapper)
    {
        Query.Select(h => new HotelSearchResult
            {
                Summary = mapper.Map<HotelSearchResult.HotelSummary>(h),
                PriceDeal = options.IncludePriceDealIfAvailable && h.Rooms.Any()
                    ? new HotelSearchResult.HotelPriceDeal
                    {
                        MinimumPriceDeal = new PriceDealResponse
                        {
                            OriginalPrice = new Price { Value = h.Rooms.Min(r => r.Price.Value) },
                            DiscountRate = h.ActiveDiscount != null
                                ? h.ActiveDiscount.Rate
                                : new DiscountRate { Percentage = 0 }
                        },
                        MaximumPriceDeal = new PriceDealResponse
                        {
                            OriginalPrice = new Price { Value = h.Rooms.Max(r => r.Price.Value) },
                            DiscountRate = h.ActiveDiscount != null
                                ? h.ActiveDiscount.Rate
                                : new DiscountRate { Percentage = 0 }
                        }
                    }
                    : null
            })
            .Include(h => h.City)
            .ApplyHotelSearchFilters(filters)
            .ApplyHotelSortingOption(options.Sorting);
    }
}