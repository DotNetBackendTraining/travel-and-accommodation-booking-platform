using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Shared.Responses;

public class PriceDealResponse
{
    public Price OriginalPrice { get; set; } = default!;
    public DiscountRate DiscountRate { get; set; } = new() { Percentage = 0 };
    public Price FinalPrice => CalculateFinalPrice();

    private Price CalculateFinalPrice()
    {
        var discountAmount = OriginalPrice.Value * (DiscountRate.Percentage / 100);
        return new Price { Value = OriginalPrice.Value - discountAmount };
    }
}