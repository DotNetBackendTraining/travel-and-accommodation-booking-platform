using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;

namespace TravelAccommodationBookingPlatform.Application.Users.Commands.LoginUser;

public record LoginUserCommand(
    string Username,
    string Password)
    : ICommand<LoginUserResponse>;