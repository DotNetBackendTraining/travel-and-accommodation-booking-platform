using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TravelAccommodationBookingPlatform.Application.Users.Commands.LoginUser;
using TravelAccommodationBookingPlatform.Application.Users.Commands.RegisterUser;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.IntegrationTests.Shared;
using TravelAccommodationBookingPlatform.Persistence;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.IntegrationTests;

[Collection("IntegrationTests")]
public class UserCommandsIntegrationTests : BaseIntegrationTest
{
    private readonly ISender _sender;
    private readonly AppDbContext _dbContext;

    public UserCommandsIntegrationTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
        _sender = Scope.ServiceProvider.GetRequiredService<ISender>();
        _dbContext = Scope.ServiceProvider.GetRequiredService<AppDbContext>();

        _dbContext.Users.RemoveRange(_dbContext.Users);
        _dbContext.SaveChanges();
    }

    private const string ValidUsername = "username";
    private const string ValidPassword = "newPassword@123";
    private const string ValidEmail = "newuser@example.com";

    private async Task RegisterUser(RegisterUserCommand command)
    {
        command.Username = ValidUsername;
        command.Password = ValidPassword;
        command.Email = ValidEmail;
        var registerResult = await _sender.Send(command);
        registerResult.IsSuccess.Should().BeTrue();
    }

    [Theory, AutoMoqData]
    public async Task Login_WithEmptyField_ShouldReturnValidationError(
        LoginUserCommand commandWithEmptyUsername,
        LoginUserCommand commandWithEmptyPassword)
    {
        commandWithEmptyUsername.Username = "";
        commandWithEmptyPassword.Password = "";

        var loginWithEmptyUsername = async () => await _sender.Send(commandWithEmptyUsername);
        var loginWithEmptyPassword = async () => await _sender.Send(commandWithEmptyPassword);

        await loginWithEmptyUsername.Should().ThrowAsync<ValidationException>();
        await loginWithEmptyPassword.Should().ThrowAsync<ValidationException>();
    }

    [Theory, AutoMoqData]
    public async Task Login_WithInvalidUsername_ShouldReturnUsernameNotFoundError(LoginUserCommand command)
    {
        command.Username = "unknown";

        var result = await _sender.Send(command);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.User.UsernameNotFound);
    }

    [Theory, AutoMoqData]
    public async Task Login_WithInvalidPassword_ShouldReturnInvalidCredentialsError(
        RegisterUserCommand registerUserCommand,
        LoginUserCommand loginUserCommand)
    {
        await RegisterUser(registerUserCommand);
        loginUserCommand.Username = ValidUsername;
        loginUserCommand.Password = "wrong_password";

        var result = await _sender.Send(loginUserCommand);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.User.InvalidCredentials);
    }

    [Theory, AutoMoqData]
    public async Task Login_WithValidCredentials_ShouldReturnToken(
        RegisterUserCommand registerUserCommand,
        LoginUserCommand loginUserCommand)
    {
        await RegisterUser(registerUserCommand);

        // Valid credentials
        loginUserCommand.Username = ValidUsername;
        loginUserCommand.Password = ValidPassword;

        // Login
        var loginResult = await _sender.Send(loginUserCommand);

        // Must succeed
        loginResult.IsSuccess.Should().BeTrue();
        loginResult.Value.Token.Should().NotBeEmpty();
    }

    [Theory, AutoMoqData]
    public async Task Register_WithValidData_ShouldAddUserSuccessfully(RegisterUserCommand command)
    {
        command.Username = ValidUsername;
        command.Password = ValidPassword;
        command.Email = ValidEmail;

        var result = await _sender.Send(command);

        result.IsSuccess.Should().BeTrue();
        _dbContext.Users.Any(u => u.Username == command.Username).Should().BeTrue();
    }

    [Theory, AutoMoqData]
    public async Task Register_WithDuplicateUsername_ShouldReturnConflict(RegisterUserCommand command)
    {
        await RegisterUser(command);

        command.Username = ValidUsername;
        command.Password = ValidPassword;
        command.Email = ValidEmail;

        // register again
        var result = await _sender.Send(command);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.User.UsernameAlreadyExists);
    }

    [Theory, AutoMoqData]
    public async Task Register_WithWeakPassword_ShouldReturnValidationError(RegisterUserCommand command)
    {
        command.Username = ValidUsername;
        command.Password = "weak_password";
        command.Email = ValidEmail;

        var action = async () => await _sender.Send(command);

        await action.Should().ThrowAsync<ValidationException>();
    }
}