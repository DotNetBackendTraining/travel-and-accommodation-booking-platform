using FluentValidation;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Shared.Validators;

public class StarRateValidator : AbstractValidator<StarRate>
{
    public StarRateValidator()
    {
        RuleFor(s => s.Rate)
            .InclusiveBetween(DomainRules.Hotels.StarRateMin, DomainRules.Hotels.StarRateMax).WithMessage(
                $"Star rate must be between {DomainRules.Hotels.StarRateMin} and {DomainRules.Hotels.StarRateMax}.");
    }
}