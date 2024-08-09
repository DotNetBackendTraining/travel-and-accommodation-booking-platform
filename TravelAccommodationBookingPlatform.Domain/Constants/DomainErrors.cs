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

        public static readonly Error CredentialsNotProvided = new(
            ErrorType.NotAuthorized,
            "User.Unauthorized",
            "No user credentials were provided");

        public static readonly Error InvalidRole = new(
            ErrorType.Forbidden,
            "User.InvalidRole",
            "User does not have the required role(s)");

        public static readonly Error CannotDeleteUserWithBookings = new(
            ErrorType.Conflict,
            "User.CannotDeleteUserWithBookings",
            "Cannot delete user as they have existing bookings");
    }

    public static class Hotel
    {
        public static readonly Error IdNotFound = new(
            ErrorType.NotFound,
            "Hotel.IdNotFound",
            "Hotel with the given ID does not exist");

        public static readonly Error CannotDeleteHotelWithBookings = new(
            ErrorType.Conflict,
            "Hotel.CannotDeleteHotelWithBookings",
            "Cannot delete hotel as it has existing bookings");
    }

    public static class Room
    {
        public static readonly Error IdNotFound = new(
            ErrorType.NotFound,
            "Room.IdNotFound",
            "Room with the given ID does not exist");

        public static readonly Error CannotDeleteRoomWithBookings = new(
            ErrorType.Conflict,
            "Room.CannotDeleteRoomWithBookings",
            "Cannot delete room as it has existing bookings");
    }

    public static class City
    {
        public static readonly Error IdNotFound = new(
            ErrorType.NotFound,
            "City.IdNotFound",
            "City with the given ID does not exist");

        public static readonly Error CannotDeleteCityWithHotels = new(
            ErrorType.Conflict,
            "City.CannotDeleteCityWithHotels",
            "Cannot delete city as it has existing hotels");
    }

    public static class Payment
    {
        public static readonly Error IdNotFound = new(
            ErrorType.NotFound,
            "Payment.IdNotFound",
            "Payment with the given ID does not exist");

        public static readonly Error CannotDeletePaymentWithBooking = new(
            ErrorType.Conflict,
            "Payment.CannotDeletePaymentWithBooking",
            "Cannot delete payment as it is associated with an existing booking");
    }

    public static class Booking
    {
        public static readonly Error IdNotFound = new(
            ErrorType.NotFound,
            "Booking.IdNotFound",
            "Booking with the given ID does not exist");

        public static readonly Error CannotDeleteBookingWithPayment = new(
            ErrorType.Conflict,
            "Booking.CannotDeleteBookingWithPayment",
            "Cannot delete booking as it has an associated payment");
    }

    public static class Discount
    {
        public static readonly Error IdNotFound = new(
            ErrorType.NotFound,
            "Discount.IdNotFound",
            "Discount with the given ID does not exist");

        public static readonly Error CannotDeleteDiscountAppliedToBooking = new(
            ErrorType.Conflict,
            "Discount.CannotDeleteDiscountAppliedToBooking",
            "Cannot delete discount as it is applied to an existing booking");
    }
}