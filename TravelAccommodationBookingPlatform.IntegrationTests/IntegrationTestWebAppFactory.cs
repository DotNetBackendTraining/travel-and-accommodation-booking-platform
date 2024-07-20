using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.MsSql;

namespace TravelAccommodationBookingPlatform.IntegrationTests;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _container = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2019-latest")
        .WithPassword("cFj23uNY0lT&WP@t")
        .Build();

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
        Environment.SetEnvironmentVariable(
            "ConnectionStrings:AppDbContextConnection",
            _container.GetConnectionString());
    }

    public new async Task DisposeAsync()
    {
        await _container.DisposeAsync();
        await base.DisposeAsync();
    }
}