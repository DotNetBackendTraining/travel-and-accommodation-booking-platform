using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Application.Interfaces.Specifications;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.Specifications;

public sealed class AvailableFiltersSpecification
    : AggregateSpecification<Hotel, HotelSearchResponse.AvailableFiltersResult>
{
    public AvailableFiltersSpecification()
    {
        Query.Select(g => new HotelSearchResponse.AvailableFiltersResult
        {
            MinimumPrice = new Price
            {
                Value = g.SelectMany(h => h.Rooms)
                    .Min(r => r.Price.Value)
            },
            MaximumPrice = new Price
            {
                Value = g.SelectMany(h => h.Rooms)
                    .Max(r => r.Price.Value)
            },
            AvailableAmenities = g.SelectMany(h => h.Amenities)
                .Distinct()
                .OrderBy(a => a)
                .ToList(),
            AvailableRoomTypes = g.SelectMany(h => h.Rooms.Select(r => r.RoomType))
                .Distinct()
                .OrderBy(r => r)
                .ToList(),
            AvailableStarRatings = g.Select(h => h.StarRate)
                .Distinct()
                .OrderBy(r => r)
                .ToList()
        });
    }
}