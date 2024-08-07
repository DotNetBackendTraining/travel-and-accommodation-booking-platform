using Ardalis.Specification;
using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.Specifications.Extensions;
using TravelAccommodationBookingPlatform.Application.Shared.Responses;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.Specifications;

public sealed class HotelSearchResultsSpecification
    : Specification<Hotel, HotelSearchResponse.HotelSearchResult>
{
    public HotelSearchResultsSpecification(
        HotelSearchQuery.HotelSearchFilters filters,
        bool includePriceDealIfAvailable,
        IMapper mapper)
    {
        Query.Select(h => new HotelSearchResponse.HotelSearchResult
            {
                HotelSummary = mapper.Map<HotelSearchResponse.HotelSummary>(h),
                PriceDeal = includePriceDealIfAvailable && h.Rooms.Any()
                    ? new HotelSearchResponse.HotelPriceDeal
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
            .OrderBy(h => h.Name);
    }
}