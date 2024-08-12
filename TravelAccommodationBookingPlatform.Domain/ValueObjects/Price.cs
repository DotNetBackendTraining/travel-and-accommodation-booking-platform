using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Domain.ValueObjects;

public class Price
{
    public double Value { get; set; }

    public Price CalculateFinalPrice(Discount? discount)
    {
        return discount is null ? this : CalculateFinalPrice(discount.Rate);
    }

    public Price CalculateFinalPrice(DiscountRate rate)
    {
        var discountAmount = Value * (rate.Percentage / 100);
        return new Price { Value = Value - discountAmount };
    }
}