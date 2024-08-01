using FluentValidation;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Shared.Validators;

public class DiscountRateValidator : AbstractValidator<DiscountRate>
{
    public DiscountRateValidator()
    {
        const double min = DomainRules.DiscountRates.PercentageMin;
        const double max = DomainRules.DiscountRates.PercentageMax;

        RuleFor(x => x.Percentage)
            .InclusiveBetween(min, max)
            .WithMessage($"Percentage must be between {min} and {max}.");
    }
}