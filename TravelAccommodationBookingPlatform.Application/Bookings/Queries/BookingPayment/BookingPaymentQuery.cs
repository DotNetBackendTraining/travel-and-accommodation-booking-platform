using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;

namespace TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingPayment;

public class BookingPaymentQuery : IQuery<BookingPaymentResponse>
{
    public Guid BookingId { get; set; }
    public Guid UserId { get; set; }
}