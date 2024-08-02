using Bogus;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Persistence.Data.Generators;

internal static class BookingDataGenerator
{
    public static List<Booking> GenerateBookings(
        IReadOnlyList<User> users,
        IReadOnlyList<Hotel> hotelsWithRooms,
        int numberOfBookings)
    {
        var faker = new Faker();
        var bookings = new List<Booking>();

        for (var i = 0; i < numberOfBookings; i++)
        {
            var user = users[faker.Random.Int(0, users.Count - 1)];
            if (user.UserRole == UserRole.Admin)
            {
                // No bookings for admins
                --i;
                continue;
            }

            var hotel = hotelsWithRooms[faker.Random.Int(0, hotelsWithRooms.Count - 1)];
            var rooms = hotel.Rooms.OrderBy(_ => faker.Random.Int()).Take(faker.Random.Int(1, 3)).ToList();

            var checkInDate = faker.Date.Future();

            var booking = new Booking
            {
                UserId = user.Id,
                Checking = new Checking
                {
                    CheckInDate = checkInDate,
                    CheckOutDate = checkInDate.AddDays(faker.Random.Int(1, 14))
                },
                Rooms = rooms,
                NumberOfGuests = new NumberOfGuests
                {
                    Adults = faker.Random.Int(1, 4),
                    Children = faker.Random.Int(0, 2)
                },
                SpecialRequest = new SpecialRequest { Request = faker.Lorem.Sentence() }
            };

            bookings.Add(booking);
        }

        return bookings;
    }
}