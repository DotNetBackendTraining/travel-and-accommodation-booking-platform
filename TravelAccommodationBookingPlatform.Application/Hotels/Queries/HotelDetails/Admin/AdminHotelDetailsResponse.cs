namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelDetails.Admin;

public class AdminHotelDetailsResponse
{
    public HotelDetailsResponse HotelDetails { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Owner { get; set; } = string.Empty;
    public int NumberOfRooms { get; set; }
}