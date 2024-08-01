using Bogus;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;
using TravelAccommodationBookingPlatform.Persistence.Data.Utility;

namespace TravelAccommodationBookingPlatform.Persistence.Data;

public class RoomDataGenerator
{
    private readonly Lazy<List<Room>> _data;

    public RoomDataGenerator(IReadOnlyList<Hotel> hotels)
    {
        _data = new Lazy<List<Room>>(() =>
        {
            var random = new Random();
            var roomTypes = Enum.GetValues(typeof(RoomType)).Cast<RoomType>().ToList();
            var rooms = new List<Room>();

            var faker = new Faker();

            foreach (var hotel in hotels)
            {
                var numberOfRooms = random.Next(5, 50);

                for (var i = 1; i <= numberOfRooms; i++)
                {
                    var roomType = roomTypes[random.Next(roomTypes.Count)];
                    var room = new Room
                    {
                        Id = Guid.NewGuid(),
                        HotelId = hotel.Id,
                        RoomNumber = i,
                        RoomType = roomType,
                        Description = $"Description for room {i}, {faker.Lorem.Sentence()}",
                        Price = new Price { Value = random.Next(2, 201) * 10 },
                        MaxNumberOfGuests = new NumberOfGuests
                        {
                            Adults = random.Next(1, 9),
                            Children = random.Next(0, 5)
                        },
                        Images = SharedDataUtility.GenerateRandomImages()
                    };

                    rooms.Add(room);
                }
            }

            return rooms;
        });
    }

    public IEnumerable<Room> Generate() => _data.Value;
}