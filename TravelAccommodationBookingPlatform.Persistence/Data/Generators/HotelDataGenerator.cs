using Bogus;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;
using TravelAccommodationBookingPlatform.Persistence.Data.Utility;

namespace TravelAccommodationBookingPlatform.Persistence.Data.Generators;

internal static class HotelDataGenerator
{
    public static List<Hotel> GenerateHotelsWithRooms(List<City> cities, int hotelsPerCity)
    {
        var faker = new Faker();
        var hotels = new List<Hotel>();
        var roomTypes = Enum.GetValues(typeof(RoomType)).Cast<RoomType>().ToList();

        foreach (var city in cities)
        {
            for (var i = 0; i < hotelsPerCity; i++)
            {
                var hotel = new Hotel
                {
                    Name = faker.Company.CompanyName(),
                    StarRate = (StarRate)faker.Random.Int(1, 5),
                    Description = faker.Lorem.Sentence(),
                    Owner = faker.Person.FullName,
                    ThumbnailImage = new Image { Url = faker.Image.PicsumUrl() },
                    CityId = city.Id,
                    Coordinates = new Coordinates
                    {
                        Latitude = faker.Address.Latitude(),
                        Longitude = faker.Address.Longitude()
                    },
                    Images = SharedDataUtility.GenerateRandomImages(),
                    Amenities = SharedDataUtility.GenerateRandomAmenities(),
                    Reviews = SharedDataUtility.GenerateRandomTexts()
                        .Select(s => new Review { Text = s })
                        .ToList(),
                    Discounts = new List<Discount>(),
                    Rooms = GenerateRooms(faker, roomTypes).ToList()
                };

                hotels.Add(hotel);
                city.Hotels.Add(hotel);
            }
        }

        return hotels;
    }

    private static List<Room> GenerateRooms(Faker faker, List<RoomType> roomTypes)
    {
        var rooms = new List<Room>();
        var numberOfRooms = faker.Random.Int(5, 50);

        for (var j = 0; j < numberOfRooms; j++)
        {
            var roomType = roomTypes[faker.Random.Int(0, roomTypes.Count - 1)];
            var room = new Room
            {
                RoomNumber = j + 1,
                RoomType = roomType,
                Description = faker.Lorem.Sentence(),
                Price = new Price { Value = faker.Random.Int(50, 500) },
                MaxNumberOfGuests = new NumberOfGuests
                {
                    Adults = faker.Random.Int(1, 4),
                    Children = faker.Random.Int(0, 2)
                },
                Images = SharedDataUtility.GenerateRandomImages()
            };
            rooms.Add(room);
        }

        return rooms;
    }
}