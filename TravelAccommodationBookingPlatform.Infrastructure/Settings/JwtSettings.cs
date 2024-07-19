namespace TravelAccommodationBookingPlatform.Infrastructure.Settings;

public class JwtSettings
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public double ExpiryHours { get; set; }
}