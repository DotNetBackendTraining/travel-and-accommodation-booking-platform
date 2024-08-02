using Microsoft.EntityFrameworkCore;
using TravelAccommodationBookingPlatform.Persistence.Data.Generators;

namespace TravelAccommodationBookingPlatform.Persistence.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Users.AnyAsync())
        {
            // latest added entity is seeded
            return;
        }

        // Order matters, don't change it
        var cities = await context.Cities.ToListAsync();
        var hotels = await context.Hotels.ToListAsync();
        var discounts = await context.Discounts.ToListAsync();
        var users = await context.Users.ToListAsync();

        if (cities.Count == 0)
        {
            cities = CityDataGenerator.GenerateCities(15);
            await context.Cities.AddRangeAsync(cities);
            await context.SaveChangesAsync();
        }

        if (hotels.Count == 0)
        {
            hotels = HotelDataGenerator.GenerateHotelsWithRooms(cities, 5);
            await context.Hotels.AddRangeAsync(hotels);
            await context.SaveChangesAsync();
        }

        if (discounts.Count == 0)
        {
            discounts = DiscountDataGenerator.GenerateDiscounts(hotels);
            await context.Discounts.AddRangeAsync(discounts);
            await context.SaveChangesAsync();
        }

        if (users.Count == 0)
        {
            users = UserDataGenerator.GenerateUsers(10);
            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }
    }
}