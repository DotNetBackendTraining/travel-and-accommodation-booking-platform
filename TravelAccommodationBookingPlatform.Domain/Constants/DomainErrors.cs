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

        public static readonly Error UsernameAlreadyExists = new(
            "User.UsernameAlreadyExists",
            "User with the given username already exists");

        public static readonly Error EmailAlreadyExists = new(
            "User.EmailAlreadyExists",
            "User with the given email already exists");
    }
}