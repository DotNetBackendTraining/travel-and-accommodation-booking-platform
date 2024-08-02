using Bogus;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Persistence.Data.Generators;

internal static class CityDataGenerator
{
    public static List<City> GenerateCities(int numberOfCities)
    {
        var faker = new Faker();
        var cities = new List<City>();

        for (var i = 0; i < numberOfCities; i++)
        {
            var city = new City
            {
                Name = faker.Address.City(),
                Country = new Country { Name = faker.Address.Country() },
                PostOffice = new PostOffice
                {
                    Address = faker.Address.StreetAddress(),
                    Description = faker.Lorem.Sentence()
                },
                ThumbnailImage = new Image { Url = faker.Image.PicsumUrl() },
                Hotels = new List<Hotel>()
            };
            cities.Add(city);
        }

        return cities;
    }
}