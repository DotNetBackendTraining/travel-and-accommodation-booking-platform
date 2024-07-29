using FluentValidation;
using TravelAccommodationBookingPlatform.Domain.Constants;

namespace TravelAccommodationBookingPlatform.Application.Shared.Validators.Extensions;

public static class CityValidationExtensions
{
    public static IRuleBuilderOptions<T, string> ValidCityName<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage("City name is required.")
            .MaximumLength(DomainRules.Cities.NameMaxLength)
            .WithMessage($"City name cannot exceed {DomainRules.Cities.NameMaxLength} characters.");
    }
}