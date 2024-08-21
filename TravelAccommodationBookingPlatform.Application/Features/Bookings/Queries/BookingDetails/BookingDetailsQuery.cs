using TravelAccommodationBookingPlatform.Application.Features.Bookings.Queries.BookingDetails.DTOs;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;

namespace TravelAccommodationBookingPlatform.Application.Features.Bookings.Queries.BookingDetails;

public class BookingDetailsQuery : IQuery<BookingDetailsResponse>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public BookingDetailsParameters Parameters { get; set; } = new();
}