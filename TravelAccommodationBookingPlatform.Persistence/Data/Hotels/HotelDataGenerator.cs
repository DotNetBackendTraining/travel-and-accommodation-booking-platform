using Newtonsoft.Json;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;
using TravelAccommodationBookingPlatform.Persistence.Data.Utility;

namespace TravelAccommodationBookingPlatform.Persistence.Data.Hotels;

public class HotelDataGenerator
{
    private readonly Lazy<List<Hotel>> _data;

    public HotelDataGenerator(IReadOnlyList<City> cities)
    {
        _data = new Lazy<List<Hotel>>(() =>
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = Path.Combine(basePath, "Data", "Hotels", "hotel_data_list.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Could not find file '{filePath}'.");
            }

            var hotelsJsonData = File.ReadAllText(filePath);
            var hotelsData = JsonConvert.DeserializeObject<List<HotelData>>(hotelsJsonData) ??
                             throw new InvalidOperationException("could not deserialize " + nameof(hotelsJsonData));

            var random = new Random();

            var hotels = hotelsData.Select(data => new Hotel
            {
                Id = Guid.Parse(data.Id),
                Name = data.Name,
                StarRate = data.StarRate,
                Description = $"Description for hotel {data.Name}, {data.Description}",
                Owner = data.Owner,
                ThumbnailImage = new Image { Url = data.ThumbnailImageUrl },
                CityId = cities[random.Next(cities.Count)].Id,
                Coordinates = new Coordinates { Latitude = data.Latitude, Longitude = data.Longitude },
                Images = SharedDataUtility.GenerateRandomImages(data.ThumbnailImageUrl),
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

public class HotelData
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int StarRate { get; set; }
    public string Description { get; set; }
    public string Owner { get; set; }
    public string ThumbnailImageUrl { get; set; }
    public string CityId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}