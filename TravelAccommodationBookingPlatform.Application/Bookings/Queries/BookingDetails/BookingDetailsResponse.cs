using TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingDetails.DTOs;
using TravelAccommodationBookingPlatform.Application.Shared.Responses;

namespace TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingDetails;

public class BookingDetailsResponse
{
    public BookingDetailsResult Booking { get; set; } = default!;
    public bool BookingHasConfirmedPayment { get; set; }
    public PriceCalculationResponse? PriceCalculation { get; set; }
    public BookingDetailsHotelResult? Hotel { get; set; }
    public List<BookingDetailsRoomResult>? Rooms { get; set; }
}