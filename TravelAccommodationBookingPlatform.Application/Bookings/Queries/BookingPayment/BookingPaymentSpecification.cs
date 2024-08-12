using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Application.Shared.Responses;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingPayment;

public sealed class BookingPaymentSpecification : Specification<Payment, BookingPaymentResponse>
{
    public BookingPaymentSpecification(Guid bookingId, Guid userId)
    {
        Query.Select(p => new BookingPaymentResponse
            {
                PaymentId = p.Id,
                PriceCalculation = new PriceCalculationResponse
                {
                    OriginalPrice = new Price { Value = p.Booking.Rooms.Sum(r => r.Price.Value) },
                    AppliedDiscount = p.AppliedDiscount
                },
                PersonalInformation = p.PersonalInformation,
                ConfirmationNumber = p.ConfirmationNumber,
                Status = p.Status
            })
            .Where(p =>
                p.BookingId == bookingId &&
                p.Booking.UserId == userId);
    }
}