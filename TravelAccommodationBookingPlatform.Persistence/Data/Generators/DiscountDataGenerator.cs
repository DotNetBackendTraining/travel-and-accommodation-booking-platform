using Bogus;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Persistence.Data.Generators;

internal static class DiscountDataGenerator
{
    public static List<Discount> GenerateDiscounts(List<Hotel> hotels)
    {
        var faker = new Faker();
        var discounts = new List<Discount>();

        foreach (var hotel in hotels)
        {
            hotel.Discounts = new List<Discount>();

            // 50% chance for hotel to have a discount
            if (faker.Random.Bool())
            {
                continue;
            }

            var discount = new Discount
            {
                Rate = new DiscountRate { Percentage = faker.Random.Int(5, 50) },
                HotelId = hotel.Id
            };
            hotel.Discounts.Add(discount);
            discounts.Add(discount);

            // 25% chance for hotel to have an active discount
            if (faker.Random.Bool())
            {
                hotel.ActiveDiscount = discount;
                hotel.ActiveDiscountId = discount.Id;
            }
        }

        return discounts;
    }
}