using Microsoft.EntityFrameworkCore;
using TravelAccommodationBookingPlatform.Persistence;
using TravelAccommodationBookingPlatform.Persistence.Data;

namespace TravelAccommodationBookingPlatform.App.WebApplicationExtensions;

public static class DatabaseAppExtensions
{
    public static async Task Migrate(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();
        await DataSeeder.SeedAsync(context);
    }
}