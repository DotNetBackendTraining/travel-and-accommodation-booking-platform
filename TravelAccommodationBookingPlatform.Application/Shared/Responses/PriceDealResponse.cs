using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Shared.Responses;

public class PriceDealResponse
{
    public Price OriginalPrice { get; set; } = default!;
    public DiscountRate DiscountRate { get; set; } = new() { Percentage = 0 };
    public Price FinalPrice => OriginalPrice.CalculateFinalPrice(DiscountRate);
}