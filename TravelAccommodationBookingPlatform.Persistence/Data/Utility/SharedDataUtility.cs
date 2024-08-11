using Bogus;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Enums;

namespace TravelAccommodationBookingPlatform.Persistence.Data.Utility;

internal static class SharedDataUtility
{
    public static List<Image> GenerateRandomImages()
    {
        var random = new Random();
        var randomNumberOfImages = random.Next(1, 10);

        var images = new List<Image>();
        for (var i = 0; i < randomNumberOfImages; i++)
        {
            images.Add(new Image { Url = new Faker().Image.PicsumUrl() });
        }

        return images;
    }

    public static List<Amenity> GenerateRandomAmenities()
    {
        var random = new Random();
        var allAmenities = Enum.GetValues(typeof(Amenity)).Cast<Amenity>().ToList();
        var randomNumberOfAmenities = random.Next(1, allAmenities.Count);
        return allAmenities.OrderBy(_ => random.Next()).Take(randomNumberOfAmenities).ToList();
    }

    public static List<string> GenerateRandomTexts()
    {
        var random = new Random();
        var faker = new Faker();
        var randomNumberOfTexts = random.Next(1, 10);
        return faker.Make(randomNumberOfTexts, () => faker.Lorem.Paragraph()).ToList();
    }
}