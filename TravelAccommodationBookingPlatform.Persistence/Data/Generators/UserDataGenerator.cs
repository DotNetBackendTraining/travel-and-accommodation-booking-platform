using Bogus;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Enums;

namespace TravelAccommodationBookingPlatform.Persistence.Data.Generators;

internal static class UserDataGenerator
{
    /// Use this username to log in as admin
    private const string AdminUsername = "admin";

    /// Use this password to log in with any user
    private const string Password = "q~>k6Dz'kD27";

    private const string PasswordHash = "$2a$11$/ol2km2/0GujhZFzGMmfd.XaOGJ64Ph7ahjZn1zLcMuuGoG2gJyPO";

    public static List<User> GenerateUsers(int numberOfUsers)
    {
        var faker = new Faker();
        var users = new List<User>();

        for (var i = 0; i < numberOfUsers; i++)
        {
            var user = new User
            {
                Email = faker.Internet.Email(),
                Username = faker.Internet.UserName(),
                PasswordHash = PasswordHash,
                UserRole = UserRole.User
            };
            users.Add(user);
        }

        users.Add(new User
        {
            Email = faker.Internet.Email(),
            Username = AdminUsername,
            PasswordHash = PasswordHash,
            UserRole = UserRole.Admin
        });

        return users;
    }
}