using TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingDetails.DTOs;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;

namespace TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingDetails;

public class BookingDetailsQuery : IQuery<BookingDetailsResponse>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public BookingDetailsParameters Parameters { get; set; } = new();
}