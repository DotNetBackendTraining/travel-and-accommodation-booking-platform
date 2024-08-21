namespace TravelAccommodationBookingPlatform.Presentation.Features.Hotels.ViewModels;

public class AdminHotelDetailsViewModel : HotelDetailsViewModel
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Owner { get; set; } = string.Empty;
    public int NumberOfRooms { get; set; }
}