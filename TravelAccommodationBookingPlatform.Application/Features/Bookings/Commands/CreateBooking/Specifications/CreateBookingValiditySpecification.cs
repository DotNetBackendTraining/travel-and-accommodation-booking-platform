using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Application.Interfaces.Specifications;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Features.Bookings.Commands.CreateBooking.Specifications;

public sealed class CreateBookingValiditySpecification : AggregateSpecification<Room, bool>
{
    public CreateBookingValiditySpecification(NumberOfGuests numberOfGuests)
    {
        Query.Select(g =>
            // All rooms belong to the same hotel
            g.All(r => r.HotelId == g.First().HotelId) &&
            // Rooms can accommodate the total number of guests
            g.Sum(r => r.MaxNumberOfGuests.Adults) >= numberOfGuests.Adults &&
            g.Sum(r => r.MaxNumberOfGuests.Children) >= numberOfGuests.Children);
    }
}