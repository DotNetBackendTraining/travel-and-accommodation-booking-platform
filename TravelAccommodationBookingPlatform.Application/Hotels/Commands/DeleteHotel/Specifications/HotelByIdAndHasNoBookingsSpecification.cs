using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Commands.DeleteHotel.Specifications;

public sealed class HotelByIdAndHasNoBookingsSpecification : Specification<Hotel>
{
    public HotelByIdAndHasNoBookingsSpecification(Guid id)
    {
        Query.Where(h => h.Id == id)
            .Where(h => h.Rooms.All(r => r.Bookings.Count == 0));
    }
}