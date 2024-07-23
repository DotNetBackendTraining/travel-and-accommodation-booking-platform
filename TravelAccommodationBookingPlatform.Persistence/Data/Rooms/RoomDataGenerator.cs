using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;
using TravelAccommodationBookingPlatform.Persistence.Data.Utility;

namespace TravelAccommodationBookingPlatform.Persistence.Data.Rooms;

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
                        Description = $"Description for room {i}, {SharedDataUtility.GenerateRandomText()}",
                        Price = new Price { Value = random.Next(2, 201) * 10 },
                        MaxNumberOfGuests = SharedDataUtility.GenerateRandomNumberOfGuests(),
                        Images = SharedDataUtility.GenerateRandomRoomImages(roomType)
                    };

                    rooms.Add(room);
                }
            }

            return rooms;
        });
    }

    public IEnumerable<Room> Generate() => _data.Value;
}