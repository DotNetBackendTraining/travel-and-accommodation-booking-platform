using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Shared.Responses;

public class PriceCalculationResponse
{
    public Price OriginalPrice { get; set; } = default!;
    public Discount? AppliedDiscount { get; set; }
    public Price FinalPrice => OriginalPrice.CalculateFinalPrice(AppliedDiscount);
}