using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Interfaces;
using TravelAccommodationBookingPlatform.Application.Interfaces.Auth;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Application.Users.Specifications;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly IMapper _mapper;
    private readonly ICudRepository<User> _cudRepository;
    private readonly IRepository<User> _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHashService _passwordHashService;

    public RegisterUserCommandHandler(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ICudRepository<User> cudRepository,
        IRepository<User> repository,
        IPasswordHashService passwordHashService)
    {
        _mapper = mapper;
        _cudRepository = cudRepository;
        _repository = repository;
        _passwordHashService = passwordHashService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var usernameSpec = new UserByUsernameSpecification(request.Username);
        var usernameExists = await _repository.ExistsAsync(usernameSpec, cancellationToken);
        if (usernameExists)
        {
            return Result.Failure(DomainErrors.User.UsernameAlreadyExists);
        }

        var emailSpec = new UserByEmailSpecification(request.Email);
        var emailExists = await _repository.ExistsAsync(emailSpec, cancellationToken);
        if (emailExists)
        {
            return Result.Failure(DomainErrors.User.EmailAlreadyExists);
        }

        var user = _mapper.Map<User>(request);
        user.PasswordHash = _passwordHashService.HashPassword(request.Password);
        user.UserRole = UserRole.User;

        _cudRepository.Add(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}