namespace TravelAccommodationBookingPlatform.Presentation.Controllers.Cities.ViewModels;

public class AdminCityDetailsViewModel : CityDetailsViewModel
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int NumberOfHotels { get; set; }
}