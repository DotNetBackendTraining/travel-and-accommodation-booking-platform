using FluentValidation;
using TravelAccommodationBookingPlatform.Domain.Constants;

namespace TravelAccommodationBookingPlatform.Application.Validation.ValidationExtensions;

public static class HotelValidationExtensions
{
    public static IRuleBuilderOptions<T, string> ValidHotelName<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Hotel name is required.")
            .MaximumLength(DomainRules.Hotels.NameMaxLength)
            .WithMessage($"Hotel name cannot be more than {DomainRules.Hotels.NameMaxLength} characters long.");
    }

    public static IRuleBuilderOptions<T, int> ValidStarRate<T>(this IRuleBuilder<T, int> ruleBuilder)
    {
        return ruleBuilder
            .InclusiveBetween(DomainRules.Hotels.StarRateMin, DomainRules.Hotels.StarRateMax)
            .WithMessage(
                $"Star rate must be between {DomainRules.Hotels.StarRateMin} and {DomainRules.Hotels.StarRateMax}.");
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