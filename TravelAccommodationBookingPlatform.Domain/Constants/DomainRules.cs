namespace TravelAccommodationBookingPlatform.Domain.Constants;

public static class DomainRules
{
    public static class Users
    {
        public const int UsernameMaxLength = 50;
        public const int PasswordMaxLength = 100;
        public const int EmailMaxLength = 256;
    }

    public static class Hotels
    {
        public const int NameMaxLength = 100;
        public const int DescriptionMaxLength = 1000;
        public const int OwnerMaxLength = 100;
        public const int StarRateMin = 1;
        public const int StarRateMax = 5;
    }

    public static class Rooms
    {
        public const int RoomNumberMin = 1;
        public const int RoomNumberMax = 9999;
        public const int DescriptionMaxLength = 500;
    }

    public static class Locations
    {
        public const double LatitudeMin = -90.0;
        public const double LatitudeMax = 90.0;
        public const double LongitudeMin = -180.0;
        public const double LongitudeMax = 180.0;
    }

    public static class Images
    {
        public const int UrlMaxLength = 2048;
    }

    public static class Reviews
    {
        public const int TextMaxLength = 2000;
    }

    public static class NumberOfGuests
    {
        public const int AdultsMin = 1;
        public const int AdultsMax = 99;
        public const int ChildrenMin = 0;
        public const int ChildrenMax = 99;
    }

    public static class Prices
    {
        public const double PriceMin = 0.0;
    }
}