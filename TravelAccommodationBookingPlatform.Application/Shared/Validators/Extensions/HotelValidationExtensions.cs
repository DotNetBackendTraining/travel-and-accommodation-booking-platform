using FluentValidation;
using TravelAccommodationBookingPlatform.Domain.Constants;

namespace TravelAccommodationBookingPlatform.Application.Shared.Validators.Extensions;

public static class HotelValidationExtensions
{
    public static IRuleBuilderOptions<T, string> ValidHotelName<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Hotel name is required.")
            .MaximumLength(DomainRules.Hotels.NameMaxLength)
            .WithMessage($"Hotel name cannot be more than {DomainRules.Hotels.NameMaxLength} characters long.");
    }

    public static IRuleBuilderOptions<T, string> ValidHotelDescription<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .MaximumLength(DomainRules.Hotels.DescriptionMaxLength)
            .WithMessage($"Description cannot be more than {DomainRules.Hotels.DescriptionMaxLength} characters long.");
    }

    public static IRuleBuilderOptions<T, string> ValidOwner<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Owner is required.")
            .MaximumLength(DomainRules.Hotels.OwnerMaxLength)
            .WithMessage($"Owner's name cannot be more than {DomainRules.Hotels.OwnerMaxLength} characters long.");
    }
}