using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Features.Hotels.Specifications;

public sealed class HotelByIdSpecification : Specification<Hotel>
{
    public HotelByIdSpecification(Guid id)
    {
        Query.Where(h => h.Id == id);
    }
}