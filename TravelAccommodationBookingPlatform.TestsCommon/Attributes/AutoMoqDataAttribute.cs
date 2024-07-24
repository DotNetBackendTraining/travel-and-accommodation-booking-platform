using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace TravelAccommodationBookingPlatform.TestsCommon.Attributes;

public class AutoMoqDataAttribute : AutoDataAttribute
{
    public AutoMoqDataAttribute(bool omitOnRecursion = false)
        : base(() => CustomizeFixture(new Fixture(), omitOnRecursion))
    {
    }

    private static Fixture CustomizeFixture(Fixture fixture, bool omitOnRecursion)
    {
        fixture.Customize(new AutoMoqCustomization());

        if (omitOnRecursion)
        {
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        return fixture;
    }
}