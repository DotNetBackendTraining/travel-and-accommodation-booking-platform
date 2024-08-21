namespace TravelAccommodationBookingPlatform.Application.Features.Bookings.Queries.BookingDetails.DTOs;

public class BookingDetailsParameters
{
    /// <summary>
    /// Indicates whether the calculated price for the booking should be included in the response.
    /// If set to true, the total price including any applicable discounts will be included.
    /// </summary>
    public bool IncludeCalculatedPrice { get; set; }

    /// <summary>
    /// Indicates whether the hotel details associated with the booking should be included in the response.
    /// If set to true, detailed information about the hotel will be included.
    /// </summary>
    public bool IncludeHotelDetails { get; set; }

    /// <summary>
    /// Indicates whether the list of rooms included in the booking should be included in the response.
    /// If set to true, details about the rooms booked will be included.
    /// </summary>
    public bool IncludeRoomsList { get; set; }
}