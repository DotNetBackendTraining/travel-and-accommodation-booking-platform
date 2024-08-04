using Ardalis.Specification;
using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Shared.Responses;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.FeaturedHotels;

public sealed class FeaturedHotelsSpecification : Specification<Hotel, FeaturedHotelsResponse.FeaturedDealResult>
{
    public FeaturedHotelsSpecification(IMapper mapper)
    {
        Query.Select(h => new FeaturedHotelsResponse.FeaturedDealResult
            {
                Hotel = mapper.Map<FeaturedHotelsResponse.FeaturedHotelSummary>(h),
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
            })
            .Include(h => h.Rooms)
            .Include(h => h.City)
            .Include(h => h.ActiveDiscount)
            .Where(h => h.ActiveDiscount != null)
            .OrderByDescending(h => h.ActiveDiscount!.Rate.Percentage)
            .ThenByDescending(h => h.StarRate);
    }
}