using AutoFixture;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.TestsCommon.Customizations;

public class RoomWithoutBookingsCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<Room>(composer => composer
            .Without(r => r.Bookings)
            .Do(r => r.Bookings = []));
    }
}