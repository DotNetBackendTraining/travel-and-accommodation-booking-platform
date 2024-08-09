using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Rooms.Specifications;

public sealed class RoomByRoomNumberSpecification : Specification<Room>
{
    public RoomByRoomNumberSpecification(Guid hotelId, int roomNumber)
    {
        Query.Where(r =>
            r.HotelId == hotelId &&
            r.RoomNumber == roomNumber);
    }
}