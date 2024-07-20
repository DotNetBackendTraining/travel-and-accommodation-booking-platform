using FluentValidation;
using TravelAccommodationBookingPlatform.Domain.Constants;

namespace TravelAccommodationBookingPlatform.Application.Users.Commands.LoginUser;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(command => command.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MaximumLength(DomainRules.Users.UsernameMaxLength)
            .WithMessage($"Username cannot be more than {DomainRules.Users.UsernameMaxLength} characters long.");

        RuleFor(command => command.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MaximumLength(DomainRules.Users.PasswordMaxLength)
            .WithMessage($"Password cannot be more than {DomainRules.Users.PasswordMaxLength} characters long.");
    }
}