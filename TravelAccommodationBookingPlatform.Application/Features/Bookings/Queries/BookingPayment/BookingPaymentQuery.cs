using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;

namespace TravelAccommodationBookingPlatform.Application.Features.Bookings.Queries.BookingPayment;

public class BookingPaymentQuery : IQuery<BookingPaymentResponse>
{
    public Guid BookingId { get; set; }
    public Guid UserId { get; set; }
}