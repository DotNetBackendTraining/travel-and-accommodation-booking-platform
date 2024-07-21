using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Interfaces.Auth;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHashService _passwordHashService;

    public RegisterUserCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        IPasswordHashService passwordHashService)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _passwordHashService = passwordHashService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetUserByUsernameAsync(request.Username, cancellationToken);
        if (existingUser is not null)
        {
            return Result.Failure(DomainErrors.User.UsernameAlreadyExists);
        }

        existingUser = await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);
        if (existingUser is not null)
        {
            return Result.Failure(DomainErrors.User.EmailAlreadyExists);
        }

        var user = _mapper.Map<User>(request);
        user.PasswordHash = _passwordHashService.HashPassword(request.Password);
        user.UserRole = UserRole.User;

        _userRepository.AddUser(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}