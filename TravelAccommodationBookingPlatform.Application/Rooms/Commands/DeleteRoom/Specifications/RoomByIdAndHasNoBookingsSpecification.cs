using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Rooms.Commands.DeleteRoom.Specifications;

public sealed class RoomByIdAndHasNoBookingsSpecification : Specification<Room>
{
    public RoomByIdAndHasNoBookingsSpecification(Guid id)
    {
        Query.Where(r => r.Id == id)
            .Where(r => r.Bookings.Count == 0);
    }
}