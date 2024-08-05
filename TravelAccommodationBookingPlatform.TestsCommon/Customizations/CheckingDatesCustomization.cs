using AutoFixture;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.TestsCommon.Customizations;

/// <summary>
/// Ensures the check-in is before check-out date.
/// And both are between past and future 10 days.
/// </summary>
public class CheckingDatesCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        var checkIn = DateTime.UtcNow.AddDays(fixture.Create<int>() % 21 - 10);
        fixture.Customize<Checking>(c => c
            .With(ch => ch.CheckInDate, checkIn)
            .With(ch => ch.CheckOutDate, checkIn.AddDays(fixture.Create<int>() % 10 + 1))
        );
    }
}