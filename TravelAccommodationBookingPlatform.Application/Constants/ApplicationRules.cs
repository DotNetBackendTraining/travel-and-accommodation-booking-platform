namespace TravelAccommodationBookingPlatform.Application.Constants;

public static class ApplicationRules
{
    public static class File
    {
        public enum ImageExtensions
        {
            Jpg = 0,
            Jpeg = 1,
            Png = 2
        }
    }

    public static class Search
    {
        public const int SearchTermMaxLength = 100;
    }
}