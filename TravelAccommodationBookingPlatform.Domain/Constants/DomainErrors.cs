using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Domain.Constants;

public static class DomainErrors
{
    public static class User
    {
        public static readonly Error UsernameNotFound = new(
            ErrorType.NotAuthorized,
            "User.UsernameNotFound",
            "User with the given username does not exist");

        public static readonly Error InvalidCredentials = new(
            ErrorType.NotAuthorized,
            "User.InvalidCredentials",
            "The given credentials were invalid");

        public static readonly Error UsernameAlreadyExists = new(
            ErrorType.Conflict,
            "User.UsernameAlreadyExists",
            "User with the given username already exists");

        public static readonly Error EmailAlreadyExists = new(
            ErrorType.Conflict,
            "User.EmailAlreadyExists",
            "User with the given email already exists");
    }

    public static class Hotel
    {
        public static readonly Error IdNotFound = new(
            ErrorType.NotFound,
            "Hotel.IdNotFound",
            "Hotel with the given ID does not exist");
    }
}