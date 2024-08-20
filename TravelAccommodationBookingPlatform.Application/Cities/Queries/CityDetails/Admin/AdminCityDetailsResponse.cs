namespace TravelAccommodationBookingPlatform.Application.Cities.Queries.CityDetails.Admin;

public class AdminCityDetailsResponse
{
    public CityDetailsResponse CityDetails { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int NumberOfHotels { get; set; }
}