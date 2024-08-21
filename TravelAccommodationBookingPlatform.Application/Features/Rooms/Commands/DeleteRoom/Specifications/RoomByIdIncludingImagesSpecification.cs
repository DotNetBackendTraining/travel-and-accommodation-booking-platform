using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Features.Rooms.Commands.DeleteRoom.Specifications;

public sealed class RoomByIdIncludingImagesSpecification : Specification<Room>
{
    public RoomByIdIncludingImagesSpecification(Guid id)
    {
        Query.Where(r => r.Id == id)
            .Include(r => r.Images);
    }
}