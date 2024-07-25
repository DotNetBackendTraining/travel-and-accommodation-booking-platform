using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelDetails;

public sealed class HotelDetailsSpecification : Specification<Hotel>
{
    public HotelDetailsSpecification(HotelDetailsQuery query)
    {
        Query.Where(h => h.Id == query.Id);
    }
}