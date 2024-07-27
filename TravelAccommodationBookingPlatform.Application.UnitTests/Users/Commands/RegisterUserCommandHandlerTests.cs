using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using Moq;
using TravelAccommodationBookingPlatform.Application.Interfaces.Auth;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Application.Users.Commands.RegisterUser;
using TravelAccommodationBookingPlatform.Application.Users.Specifications;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.Application.UnitTests.Users.Commands;

public class RegisterUserCommandHandlerTests
{
    [Theory, AutoMoqData]
    public async Task Handle_Fails_WhenUsernameAlreadyExists(
        [Frozen] Mock<IRepository<User>> mockUserRepository,
        RegisterUserCommand command,
        User existingUser,
        RegisterUserCommandHandler handler)
    {
        command.Username = existingUser.Username;
        mockUserRepository.Setup(repo => repo.ExistsAsync(
                It.IsAny<UserByUsernameSpecification>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(DomainErrors.User.UsernameAlreadyExists);
    }

    [Theory, AutoMoqData]
    public async Task Handle_Fails_WhenEmailAlreadyExists(
        [Frozen] Mock<IRepository<User>> mockUserRepository,
        RegisterUserCommand command,
        User existingUser,
        RegisterUserCommandHandler handler)
    {
        command.Email = existingUser.Email;
        mockUserRepository.Setup(repo => repo.ExistsAsync(
                It.IsAny<UserByUsernameSpecification>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        mockUserRepository.Setup(repo => repo.ExistsAsync(
                It.IsAny<UserByEmailSpecification>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(DomainErrors.User.EmailAlreadyExists);
    }

    [Theory, AutoMoqData]
    public async Task Handle_Succeeds_WhenUserIsRegistered(
        [Frozen] Mock<IMapper> mockMapper,
        [Frozen] Mock<IRepository<User>> mockUserRepository,
        [Frozen] Mock<ICudRepository<User>> mockCudRepository,
        [Frozen] Mock<IPasswordHashService> mockPasswordHashService,
        [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
        RegisterUserCommand command,
        User mappedUser,
        RegisterUserCommandHandler handler)
    {
        mockUserRepository.Setup(repo => repo.ExistsAsync(
                It.IsAny<UserByUsernameSpecification>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        mockUserRepository.Setup(repo => repo.ExistsAsync(
                It.IsAny<UserByEmailSpecification>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        mockMapper.Setup(m => m.Map<User>(command))
            .Returns(mappedUser);
        mockPasswordHashService.Setup(service => service.HashPassword(command.Password))
            .Returns("hashed_password");

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        mockCudRepository.Verify(repo => repo.Add(mappedUser), Times.Once);
        mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}