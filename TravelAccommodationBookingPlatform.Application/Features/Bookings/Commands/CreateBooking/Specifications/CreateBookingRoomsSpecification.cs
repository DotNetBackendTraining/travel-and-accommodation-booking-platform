using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Features.Bookings.Commands.CreateBooking.Specifications;

public sealed class CreateBookingRoomsSpecification : Specification<Room>
{
    public CreateBookingRoomsSpecification(IEnumerable<Guid> roomIds)
    {
        Query.Where(r => roomIds.Contains(r.Id))
            .AsTracking();
    }
}