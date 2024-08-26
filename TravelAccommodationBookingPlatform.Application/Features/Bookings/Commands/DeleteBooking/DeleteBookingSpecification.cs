using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Features.Bookings.Commands.DeleteBooking;

public sealed class DeleteBookingSpecification : Specification<Booking>
{
    public DeleteBookingSpecification(Guid userId, Guid bookingId)
    {
        Query.Where(b =>
                b.Id == bookingId &&
                b.UserId == userId)
            .Include(b => b.Payment);
    }
}