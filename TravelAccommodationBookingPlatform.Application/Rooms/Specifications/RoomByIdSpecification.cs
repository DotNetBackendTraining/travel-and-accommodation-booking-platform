using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Rooms.Specifications;

public sealed class RoomByIdSpecification : Specification<Room>
{
    public RoomByIdSpecification(Guid id)
    {
        Query.Where(r => r.Id == id);
    }
}