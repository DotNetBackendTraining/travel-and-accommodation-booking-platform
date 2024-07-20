using FluentValidation;
using TravelAccommodationBookingPlatform.Application.ValidationExtensions;
using TravelAccommodationBookingPlatform.Domain.Constants;

namespace TravelAccommodationBookingPlatform.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(command => command.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(DomainRules.Users.UsernameMaxLength)
            .WithMessage($"Username cannot be more than {DomainRules.Users.UsernameMaxLength} characters long.");

        RuleFor(command => command.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MaximumLength(DomainRules.Users.PasswordMaxLength)
            .WithMessage($"Password cannot be more than {DomainRules.Users.PasswordMaxLength} characters long.")
            .StrongPassword();

        RuleFor(command => command.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(DomainRules.Users.EmailMaxLength)
            .WithMessage($"Email cannot be more than {DomainRules.Users.EmailMaxLength} characters long.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}