using FluentValidation;
using TravelAccommodationBookingPlatform.Application.Validators.Extensions;

namespace TravelAccommodationBookingPlatform.Application.Features.Users.Commands.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(command => command.Username).ValidUsername();
        RuleFor(command => command.Password).ValidAndStrongPassword();
        RuleFor(command => command.Email).ValidEmail();
    }
}