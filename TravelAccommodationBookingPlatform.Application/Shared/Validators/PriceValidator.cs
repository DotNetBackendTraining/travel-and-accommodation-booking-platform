using FluentValidation;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Shared.Validators;

public class PriceValidator : AbstractValidator<Price>
{
    public PriceValidator()
    {
        RuleFor(x => x.Value)
            .GreaterThanOrEqualTo(DomainRules.Prices.PriceMin)
            .WithMessage($"Price must be at least {DomainRules.Prices.PriceMin}.");
    }
}