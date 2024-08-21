using TravelAccommodationBookingPlatform.Application.Shared.Responses;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Features.Bookings.Queries.BookingPayment;

public class BookingPaymentResponse
{
    public Guid PaymentId { get; set; }
    public PriceCalculationResponse PriceCalculation { get; set; } = default!;
    public PersonalInformation PersonalInformation { get; set; } = default!;
    public string ConfirmationNumber { get; set; } = string.Empty;
    public PaymentStatus Status { get; set; }
}