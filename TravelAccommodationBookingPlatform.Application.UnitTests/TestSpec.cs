using Ardalis.Specification;

namespace TravelAccommodationBookingPlatform.Application.UnitTests;

public sealed class TestSpec<T> : Specification<T>
{
    public TestSpec(Action<ISpecificationBuilder<T>> configure)
    {
        configure(Query);
    }
}