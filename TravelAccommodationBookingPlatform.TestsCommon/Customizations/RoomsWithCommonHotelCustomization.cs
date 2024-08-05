using AutoFixture;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.TestsCommon.Customizations;

/// <summary>
/// Ensures that any created room will share the same hotel entity (and hotel id).
/// </summary>
public class RoomsWithCommonHotelCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var commonHotel = fixture.Create<Hotel>();
        fixture.Customize<Room>(c => c
            .With(r => r.HotelId, commonHotel.Id)
            .With(r => r.Hotel, commonHotel)
        );
    }
}