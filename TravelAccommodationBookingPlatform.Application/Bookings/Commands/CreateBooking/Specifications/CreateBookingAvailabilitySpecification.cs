using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Application.Interfaces.Specifications;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Bookings.Commands.CreateBooking.Specifications;

public sealed class CreateBookingAvailabilitySpecification : AggregateSpecification<Room, bool>
{
    public CreateBookingAvailabilitySpecification(Checking checking)
    {
        // All rooms are available during checking time
        Query.Select(g => g.All(r =>
            !r.Bookings.Any(b =>
                b.Checking.CheckInDate <= checking.CheckOutDate &&
                b.Checking.CheckOutDate >= checking.CheckInDate)));
    }
}