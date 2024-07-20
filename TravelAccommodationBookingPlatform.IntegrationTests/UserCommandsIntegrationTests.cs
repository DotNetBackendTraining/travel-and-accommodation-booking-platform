using FluentAssertions;
using TravelAccommodationBookingPlatform.Application.Users.Commands.LoginUser;
using TravelAccommodationBookingPlatform.Application.Users.Commands.RegisterUser;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Shared;
using TravelAccommodationBookingPlatform.TestsCommon.Attributes;

namespace TravelAccommodationBookingPlatform.IntegrationTests;

public class UserCommandsIntegrationTests : BaseIntegrationTest
{
    public UserCommandsIntegrationTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Theory, AutoMoqData]
    public async Task Login_WithEmptyField_ShouldReturnValidationError(
        LoginUserCommand commandWithEmptyUsername,
        LoginUserCommand commandWithEmptyPassword)
    {
        commandWithEmptyUsername.Username = "";
        commandWithEmptyPassword.Password = "";

        var resultOfEmptyUsername = await Sender.Send(commandWithEmptyUsername);
        var resultOfEmptyPassword = await Sender.Send(commandWithEmptyPassword);

        resultOfEmptyUsername.IsFailure.Should().BeTrue();
        resultOfEmptyUsername.Should().BeAssignableTo<IValidationResult>();
        resultOfEmptyPassword.IsFailure.Should().BeTrue();
        resultOfEmptyPassword.Should().BeAssignableTo<IValidationResult>();
    }

    [Theory, AutoMoqData]
    public async Task Login_WithInvalidUsername_ShouldReturnUsernameNotFoundError(LoginUserCommand command)
    {
        command.Username = "unknown";

        var result = await Sender.Send(command);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.User.UsernameNotFound);
    }

    [Theory, AutoMoqData]
    public async Task LoginThenRegisterScenario(
        RegisterUserCommand registerCommand,
        LoginUserCommand loginCommand)
    {
        /* Register User */

        registerCommand.Username = "newUser";
        registerCommand.Password = "StrongPassword!234";
        registerCommand.Email = "newuser@example.com";

        var registerResult = await Sender.Send(registerCommand);

        registerResult.IsSuccess.Should().BeTrue();
        DbContext.Users.Any(u => u.Username == registerCommand.Username).Should().BeTrue();

        /* Attempt Registration Again */

        var registrationAgainResult = await Sender.Send(registerCommand);

        registrationAgainResult.IsFailure.Should().BeTrue();
        registrationAgainResult.Error.Should().Be(DomainErrors.User.UsernameAlreadyExists);

        /* Login with Registered User */

        loginCommand.Username = registerCommand.Username;
        loginCommand.Password = registerCommand.Password;

        var loginResult = await Sender.Send(loginCommand);

        loginResult.IsSuccess.Should().BeTrue();
        loginResult.Value.Token.Should().NotBeEmpty();

        /* Attempt Login with Invalid Password */

        loginCommand.Password = "WrongPassword123";

        var failedLoginResult = await Sender.Send(loginCommand);

        failedLoginResult.IsFailure.Should().BeTrue();
        failedLoginResult.Error.Should().Be(DomainErrors.User.InvalidCredentials);
    }
}