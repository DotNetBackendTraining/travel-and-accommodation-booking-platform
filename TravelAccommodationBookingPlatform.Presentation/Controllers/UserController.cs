using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Application.Users.Commands.LoginUser;
using TravelAccommodationBookingPlatform.Domain.Shared;
using TravelAccommodationBookingPlatform.Presentation.Controllers.ResultExtensions;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly ISender _sender;

    public UserController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Authenticates a user and generates a token.
    /// </summary>
    /// <param name="command">Login command with user credentials.</param>
    /// <returns>A token if login is successful; otherwise, an error.</returns>
    /// <response code="200">Returns the newly created token.</response>
    /// <response code="422">If the request is invalid (validation error).</response>
    /// <response code="401">Unauthorized if credentials are incorrect.</response>
    [HttpPost("token")]
    [ProducesResponseType(typeof(LoginUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
    {
        var result = await _sender.Send(command);
        if (result is IValidationResult validationResult)
        {
            return validationResult.ToValidationProblemDetails();
        }

        return result.IsFailure
            ? result.ToUnauthorizedProblemDetails()
            : Ok(result.Value);
    }
}