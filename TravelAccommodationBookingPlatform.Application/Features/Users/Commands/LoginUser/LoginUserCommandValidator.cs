using FluentValidation;
using TravelAccommodationBookingPlatform.Application.Validators.Extensions;

namespace TravelAccommodationBookingPlatform.Application.Features.Users.Commands.LoginUser;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(command => command.Username).ValidUsername();
        RuleFor(command => command.Password).ValidPassword();
    }
}