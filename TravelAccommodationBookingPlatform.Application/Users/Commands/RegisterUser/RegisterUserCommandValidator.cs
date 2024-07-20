using FluentValidation;
using TravelAccommodationBookingPlatform.Application.ValidationExtensions;

namespace TravelAccommodationBookingPlatform.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(command => command.Username).ValidUsername();
        RuleFor(command => command.Password).ValidAndStrongPassword();
        RuleFor(command => command.Email).ValidEmail();
    }
}