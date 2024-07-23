using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.Data.Cities;

public class CityDataGenerator
{
    private readonly Lazy<List<City>> _data = new(() =>
    {
        var cities = new List<City>();
        for (var i = 0; i < 15; i++)
        {
            cities.Add(new City());
        }

        return cities;
    });

    public IEnumerable<City> Generate() => _data.Value;
}