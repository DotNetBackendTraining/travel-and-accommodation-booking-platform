using Microsoft.EntityFrameworkCore;
using TravelAccommodationBookingPlatform.Persistence.Data.Generators;

namespace TravelAccommodationBookingPlatform.Persistence.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Payments.AnyAsync())
        {
            // latest added entity is seeded
            return;
        }

        // Order matters, don't change it
        var cities = await context.Cities.ToListAsync();
        var hotelsWithRooms = await context.Hotels.Include(h => h.Rooms).ToListAsync();
        var discounts = await context.Discounts.ToListAsync();
        var users = await context.Users.ToListAsync();
        var bookings = await context.Bookings.ToListAsync();
        var payments = await context.Payments.ToListAsync();

        if (cities.Count == 0)
        {
            cities = CityDataGenerator.GenerateCities(15);
            await context.Cities.AddRangeAsync(cities);
            await context.SaveChangesAsync();
        }

        if (hotelsWithRooms.Count == 0)
        {
            hotelsWithRooms = HotelDataGenerator.GenerateHotelsWithRooms(cities, 5);
            await context.Hotels.AddRangeAsync(hotelsWithRooms);
            await context.SaveChangesAsync();
        }

        if (discounts.Count == 0)
        {
            discounts = DiscountDataGenerator.GenerateDiscounts(hotelsWithRooms);
            await context.Discounts.AddRangeAsync(discounts);
            await context.SaveChangesAsync();
        }

        if (users.Count == 0)
        {
            users = UserDataGenerator.GenerateUsers(10);
            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();
        }

        if (bookings.Count == 0)
        {
            bookings = BookingDataGenerator.GenerateBookings(users, hotelsWithRooms, 30);
            await context.Bookings.AddRangeAsync(bookings);
            await context.SaveChangesAsync();
        }

        if (payments.Count == 0)
        {
            payments = PaymentDataGenerator.GeneratePayments(bookings);
            await context.Payments.AddRangeAsync(payments);
            await context.SaveChangesAsync();
        }
    }
}