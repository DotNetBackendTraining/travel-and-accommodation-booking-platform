using FluentValidation;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Shared.Validators;

public class CountryValidator : AbstractValidator<Country>
{
    public CountryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Country name is required.")
            .MaximumLength(DomainRules.Countries.NameMaxLength)
            .WithMessage($"Country name cannot exceed {DomainRules.Countries.NameMaxLength} characters.");
    }
}