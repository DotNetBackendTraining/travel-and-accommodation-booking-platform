using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Domain.Constants;

public static class DomainErrors
{
    public static class User
    {
        public static readonly Error UsernameNotFound = new(
            "User.UsernameNotFound",
            "User with the given username does not exist");

        public static readonly Error InvalidCredentials = new(
            "User.InvalidCredentials",
            "The given credentials were invalid");
    }
}