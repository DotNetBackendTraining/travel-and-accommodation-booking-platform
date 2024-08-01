using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace TravelAccommodationBookingPlatform.TestsCommon.Attributes;

public class AutoMoqDataAttribute : AutoDataAttribute
{
    public AutoMoqDataAttribute(bool omitOnRecursion = false)
        : base(() => CustomizeFixture(new Fixture(), omitOnRecursion, []))
    {
    }

    public AutoMoqDataAttribute(bool omitOnRecursion = false, params Type[] customizations)
        : base(() => CustomizeFixture(new Fixture(), omitOnRecursion, customizations))
    {
    }

    private static Fixture CustomizeFixture(Fixture fixture, bool omitOnRecursion, IEnumerable<Type> customizations)
    {
        fixture.Customize(new AutoMoqCustomization());

        if (omitOnRecursion)
        {
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        foreach (var customization in customizations)
        {
            if (typeof(ICustomization).IsAssignableFrom(customization))
            {
                var instance = Activator.CreateInstance(customization) ??
                               throw new InvalidOperationException("Could not create customization instance");
                fixture.Customize((ICustomization)instance);
            }
        }

        return fixture;
    }
}