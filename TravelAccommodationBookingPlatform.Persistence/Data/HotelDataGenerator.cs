using Bogus;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;
using TravelAccommodationBookingPlatform.Persistence.Data.Utility;

namespace TravelAccommodationBookingPlatform.Persistence.Data;

public class HotelDataGenerator
{
    private readonly Lazy<List<Hotel>> _data;

    public HotelDataGenerator(IReadOnlyList<City> cities)
    {
        _data = new Lazy<List<Hotel>>(() =>
        {
            var random = new Random();

            var faker = new Faker<HotelData>()
                .RuleFor(h => h.Id, f => f.Random.Guid().ToString())
                .RuleFor(h => h.Name, f => f.Company.CompanyName())
                .RuleFor(h => h.StarRate, f => f.Random.Int(1, 5))
                .RuleFor(h => h.Description, f => f.Lorem.Sentence())
                .RuleFor(h => h.Owner, f => f.Person.FullName)
                .RuleFor(h => h.ThumbnailImageUrl, f => f.Image.PicsumUrl())
                .RuleFor(h => h.Latitude, f => f.Address.Latitude())
                .RuleFor(h => h.Longitude, f => f.Address.Longitude());

            var hotelsData = faker.Generate(50);

            var hotels = hotelsData.Select(data => new Hotel
            {
                Id = Guid.Parse(data.Id),
                Name = data.Name,
                StarRate = (StarRate)data.StarRate,
                Description = $"Description for hotel {data.Name}, {data.Description}",
                Owner = data.Owner,
                ThumbnailImage = new Image { Url = data.ThumbnailImageUrl },
                CityId = cities[random.Next(cities.Count)].Id,
                Coordinates = new Coordinates { Latitude = data.Latitude, Longitude = data.Longitude },
                Images = SharedDataUtility.GenerateRandomImages(),
                Amenities = SharedDataUtility.GenerateRandomAmenities(),
                Reviews = SharedDataUtility.GenerateRandomTexts()
                    .Select(s => new Review { Text = s })
                    .ToList()
            }).ToList();

            return hotels;
        });
    }

    public IEnumerable<Hotel> Generate() => _data.Value;
}

internal class HotelData
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int StarRate { get; set; }
    public string Description { get; set; }
    public string Owner { get; set; }
    public string ThumbnailImageUrl { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}