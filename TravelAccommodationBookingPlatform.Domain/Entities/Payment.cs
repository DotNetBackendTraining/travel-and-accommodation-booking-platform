using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Domain.Entities;

public class Payment : BaseEntity
{
    public Guid BookingId { get; set; }
    public Booking Booking { get; set; } = default!;
    public Guid? AppliedDiscountId { get; set; }
    public Discount? AppliedDiscount { get; set; }
    public PersonalInformation PersonalInformation { get; set; } = default!;
    public string ConfirmationNumber { get; set; } = string.Empty;
    public PaymentStatus Status { get; set; }
}