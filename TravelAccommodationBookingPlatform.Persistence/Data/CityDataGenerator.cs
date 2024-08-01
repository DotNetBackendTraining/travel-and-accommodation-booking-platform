using Bogus;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Persistence.Data;

public class CityDataGenerator
{
    private readonly Lazy<List<City>> _data;

    public CityDataGenerator()
    {
        _data = new Lazy<List<City>>(() =>
        {
            var faker = new Faker<CityData>()
                .RuleFor(c => c.Name, f => f.Address.City())
                .RuleFor(c => c.CountryName, f => f.Address.Country())
                .RuleFor(c => c.PostOfficeAddress, f => f.Address.StreetAddress());

            var citiesData = faker.Generate(15);

            var cities = citiesData.Select(data => new City
            {
                Name = data.Name,
                Country = new Country { Name = data.CountryName },
                PostOffice = new PostOffice
                {
                    Address = data.PostOfficeAddress,
                    Description = $"Description for {data.Name} Post Office"
                },
                ThumbnailImage = new Image { Url = new Faker().Image.PicsumUrl() },
            }).ToList();

            return cities;
        });
    }

    public IEnumerable<City> Generate() => _data.Value;
}

internal class CityData
{
    public string Name { get; set; }
    public string CountryName { get; set; }
    public string PostOfficeAddress { get; set; }
}