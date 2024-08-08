using Microsoft.Extensions.DependencyInjection;

namespace TravelAccommodationBookingPlatform.IntegrationTests.Shared;

public abstract class BaseIntegrationTest : IDisposable
{
    protected readonly IServiceScope Scope;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        Scope = factory.Services.CreateScope();
    }

    public void Dispose()
    {
        Scope.Dispose();
    }
}