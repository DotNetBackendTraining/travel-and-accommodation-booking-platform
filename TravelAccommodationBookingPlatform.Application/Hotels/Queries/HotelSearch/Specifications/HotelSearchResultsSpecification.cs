using Ardalis.Specification;
using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.Specifications.Extensions;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.Specifications;

public sealed class HotelSearchResultsSpecification
    : Specification<Hotel, HotelSearchResponse.HotelSearchResult>
{
    public HotelSearchResultsSpecification(HotelSearchQuery.HotelSearchFilters filters, IMapper mapper)
    {
        Query.Select(h => new HotelSearchResponse.HotelSearchResult
            {
                HotelSummary = mapper.Map<HotelSearchResponse.HotelSummary>(h),
                MinimumPrice = new Price { Value = h.Rooms.Min(r => r.Price.Value) },
                MaximumPrice = new Price { Value = h.Rooms.Max(r => r.Price.Value) }
            })
            .Include(h => h.City)
            .ApplyHotelSearchFilters(filters)
            .OrderBy(h => h.Name);
    }
}