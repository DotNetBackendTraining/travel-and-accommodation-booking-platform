using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Rooms.Queries.RoomDetails;

public sealed class RoomDetailsSpecification : Specification<Room>
{
    public RoomDetailsSpecification(RoomDetailsQuery query)
    {
        Query.Where(r => r.Id == query.Id);
    }
}