using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Testcontainers.MsSql;

namespace TravelAccommodationBookingPlatform.IntegrationTests.Shared;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _container = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2017-latest")
        .Build();

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
        await WaitForContainerToBeReady(_container);
        Environment.SetEnvironmentVariable(
            "ConnectionStrings:AppDbContextConnection",
            _container.GetConnectionString());
    }

    public new async Task DisposeAsync()
    {
        await _container.DisposeAsync();
        await base.DisposeAsync();
    }

    private static async Task WaitForContainerToBeReady(IDatabaseContainer container)
    {
        var timeout = TimeSpan.FromMinutes(5);
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        while (stopwatch.Elapsed < timeout)
        {
            try
            {
                await using var connection = new SqlConnection(container.GetConnectionString());
                await connection.OpenAsync();
                return;
            }
            catch
            {
                // Wait for a bit before retrying
                await Task.Delay(TimeSpan.FromSeconds(10));
            }
        }

        throw new Exception("The container did not become ready within the specified timeout.");
    }
}