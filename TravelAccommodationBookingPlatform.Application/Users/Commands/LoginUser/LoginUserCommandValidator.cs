using FluentValidation;
using TravelAccommodationBookingPlatform.Application.Validation.ValidationExtensions;

namespace TravelAccommodationBookingPlatform.Application.Users.Commands.LoginUser;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(command => command.Username).ValidUsername();
        RuleFor(command => command.Password).ValidPassword();
    }
}