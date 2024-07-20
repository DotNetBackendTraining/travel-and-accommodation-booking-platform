using TravelAccommodationBookingPlatform.Application.Interfaces.Authentication;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Users.Commands.LoginUser;

public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, LoginUserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHashService passwordHashService,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHashService = passwordHashService;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Result<LoginUserResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByUsernameAsync(request.Username, cancellationToken);
        if (user is null)
        {
            return Result.Failure<LoginUserResponse>(DomainErrors.User.UsernameNotFound);
        }

        var verified = _passwordHashService.VerifyPassword(request.Password, user.PasswordHash);
        if (!verified)
        {
            return Result.Failure<LoginUserResponse>(DomainErrors.User.InvalidCredentials);
        }

        var token = _jwtTokenGenerator.GenerateToken(user);
        return Result.Success(new LoginUserResponse(token));
    }
}