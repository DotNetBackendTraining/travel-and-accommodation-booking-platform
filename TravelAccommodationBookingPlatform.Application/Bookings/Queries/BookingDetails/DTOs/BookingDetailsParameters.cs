namespace TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingDetails.DTOs;

public class BookingDetailsParameters
{
    public bool IncludeCalculatedPrice { get; set; }
    public bool IncludeHotelDetails { get; set; }
    public bool IncludeRoomsList { get; set; }
}