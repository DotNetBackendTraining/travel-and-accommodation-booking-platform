using FluentValidation;
using TravelAccommodationBookingPlatform.Domain.Constants;

namespace TravelAccommodationBookingPlatform.Application.Shared.Validators.Extensions;

public static class UserValidationExtensions
{
    public static IRuleBuilderOptions<T, string> ValidUsername<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(DomainRules.Users.UsernameMaxLength)
            .WithMessage($"Username cannot be more than {DomainRules.Users.UsernameMaxLength} characters long.");
    }

    public static IRuleBuilderOptions<T, string> ValidPassword<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("Password is required.")
            .MaximumLength(DomainRules.Users.PasswordMaxLength)
            .WithMessage($"Password cannot be more than {DomainRules.Users.PasswordMaxLength} characters long.");
    }

    public static IRuleBuilderOptions<T, string> ValidEmail<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .MaximumLength(DomainRules.Users.EmailMaxLength)
            .WithMessage($"Email cannot be more than {DomainRules.Users.EmailMaxLength} characters long.")
            .EmailAddress().WithMessage("Invalid email format.");
    }

    public static IRuleBuilderOptions<T, string> ValidAndStrongPassword<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .ValidPassword()
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches("[0-9]").WithMessage("Password must contain at least one number.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
    }
}