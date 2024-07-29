using Newtonsoft.Json;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;
using TravelAccommodationBookingPlatform.Persistence.Data.Utility;

namespace TravelAccommodationBookingPlatform.Persistence.Data.Cities;

public class CityDataGenerator
{
    private readonly Lazy<List<City>> _data;

    public CityDataGenerator()
    {
        _data = new Lazy<List<City>>(() =>
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = Path.Combine(basePath, "Data", "Cities", "city_data_list.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Could not find file '{filePath}'.");
            }

            var citiesJsonData = File.ReadAllText(filePath);
            var citiesData = JsonConvert.DeserializeObject<List<CityData>>(citiesJsonData) ??
                             throw new InvalidOperationException("Could not deserialize " + nameof(citiesJsonData));

            var cities = citiesData.Select(data => new City
            {
                Name = data.Name,
                Country = new Country { Name = data.CountryName },
                PostOffice = new PostOffice
                {
                    Address = data.PostOfficeAddress,
                    Description = $"Description for {data.Name} Post Office"
                },
                ThumbnailImage = new Image { Url = SharedDataUtility.ExampleCityImageUrl() },
            }).ToList();

            return cities;
        });
    }

    public IEnumerable<City> Generate() => _data.Value;
}

public class CityData
{
    public string Name { get; set; }
    public string CountryName { get; set; }
    public string PostOfficeAddress { get; set; }
}