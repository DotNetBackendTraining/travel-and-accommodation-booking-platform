using Microsoft.EntityFrameworkCore;
using TravelAccommodationBookingPlatform.Persistence;
using TravelAccommodationBookingPlatform.Persistence.Data.Cities;
using TravelAccommodationBookingPlatform.Persistence.Data.Hotels;
using TravelAccommodationBookingPlatform.Persistence.Data.Rooms;

namespace TravelAccommodationBookingPlatform.App.WebApplicationExtensions;

public static class DatabaseAppExtensions
{
    public static async Task Migrate(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await context.Database.MigrateAsync();

        if (context.Rooms.Any())
        {
            return;
        }

        // Order matters, don't change it
        var cities = context.Cities.ToList();
        var hotels = context.Hotels.ToList();
        var rooms = context.Rooms.ToList();

        if (cities.Count == 0)
        {
            var generator = new CityDataGenerator();
            cities = generator.Generate().ToList();
            await context.Cities.AddRangeAsync(cities);
        }

        if (hotels.Count == 0)
        {
            var generator = new HotelDataGenerator(cities);
            hotels = generator.Generate().ToList();
            await context.Hotels.AddRangeAsync(hotels);
        }

        if (rooms.Count == 0)
        {
            var generator = new RoomDataGenerator(hotels);
            rooms = generator.Generate().ToList();
            await context.Rooms.AddRangeAsync(rooms);
        }

        await context.SaveChangesAsync();
    }
}