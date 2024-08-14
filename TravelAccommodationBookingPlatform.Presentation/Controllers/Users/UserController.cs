using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Application.Users.Commands.LoginUser;
using TravelAccommodationBookingPlatform.Application.Users.Commands.RegisterUser;
using TravelAccommodationBookingPlatform.Presentation.Attributes;
using TravelAccommodationBookingPlatform.Presentation.Controllers.Users.Requests;
using TravelAccommodationBookingPlatform.Presentation.Shared;
using TravelAccommodationBookingPlatform.Presentation.Shared.ResultExtensions;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers.Users;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/users")]
public class UserController : AbstractController
{
    public UserController(ISender sender, IMapper mapper) : base(sender, mapper)
    {
    }

    /// <summary>
    /// Authenticates a user and generates a token.
    /// </summary>
    /// <param name="request">Login request with user credentials.</param>
    /// <param name="cancellationToken">Cancellation token for the request</param>
    /// <returns>A token if login is successful; otherwise, an error.</returns>
    /// <response code="200">Returns the newly created token.</response>
    /// <response code="422">If the request is invalid (validation error).</response>
    /// <response code="401">Unauthorized if credentials are incorrect.</response>
    [HttpPost("token")]
    [ProducesResponseType(typeof(LoginUserResponse), StatusCodes.Status200OK)]
    [ProducesError(StatusCodes.Status422UnprocessableEntity)]
    [ProducesError(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginUserResponse>> Login(
        [FromForm] LoginUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = Mapper.Map<LoginUserCommand>(request);
        var result = await Sender.Send(command, cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblemDetails();
    }

    /// <summary>
    /// Registers a new user in the system.
    /// </summary>
    /// <param name="request">Register user request with user details.</param>
    /// <param name="cancellationToken">Cancellation token for the request</param>
    /// <returns>Response indicating the result of the registration process.</returns>
    /// <response code="201">User created successfully.</response>
    /// <response code="422">If the request is invalid (validation error).</response>
    /// <response code="409">Conflict if credentials causes a conflict.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesError(StatusCodes.Status422UnprocessableEntity)]
    [ProducesError(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register(
        [FromForm] RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = Mapper.Map<RegisterUserCommand>(request);
        var result = await Sender.Send(command, cancellationToken);
        return result.IsSuccess
            ? Created()
            : result.ToProblemDetails();
    }
}