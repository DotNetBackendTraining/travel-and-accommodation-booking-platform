using Microsoft.EntityFrameworkCore;
using TravelAccommodationBookingPlatform.Persistence.Data.Generators;

namespace TravelAccommodationBookingPlatform.Persistence.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Rooms.AnyAsync())
        {
            // latest added entity is seeded
            return;
        }

        // Order matters, don't change it
        var cities = await context.Cities.ToListAsync();
        var hotels = await context.Hotels.ToListAsync();

        if (cities.Count == 0)
        {
            cities = CityDataGenerator.GenerateCities(15);
            context.Cities.AddRange(cities);
            await context.SaveChangesAsync();
        }

        if (hotels.Count == 0)
        {
            hotels = HotelDataGenerator.GenerateHotelsWithRooms(cities, 5);
            context.Hotels.AddRange(hotels);
            await context.SaveChangesAsync();
        }
    }
}