using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Application.Users.Commands.LoginUser;
using TravelAccommodationBookingPlatform.Application.Users.Commands.RegisterUser;
using TravelAccommodationBookingPlatform.Presentation.Controllers.ResultExtensions;
using TravelAccommodationBookingPlatform.Presentation.Requests;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public UserController(
        ISender sender,
        IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    /// <summary>
    /// Authenticates a user and generates a token.
    /// </summary>
    /// <param name="request">Login request with user credentials.</param>
    /// <returns>A token if login is successful; otherwise, an error.</returns>
    /// <response code="200">Returns the newly created token.</response>
    /// <response code="422">If the request is invalid (validation error).</response>
    /// <response code="401">Unauthorized if credentials are incorrect.</response>
    [HttpPost("token")]
    [ProducesResponseType(typeof(LoginUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
    {
        var command = _mapper.Map<LoginUserCommand>(request);
        var result = await _sender.Send(command);
        return result.IsFailure
            ? result.ToUnauthorizedProblemDetails()
            : Ok(result.Value);
    }

    /// <summary>
    /// Registers a new user in the system.
    /// </summary>
    /// <param name="request">Register user request with user details.</param>
    /// <returns>Response indicating the result of the registration process.</returns>
    /// <response code="201">User created successfully.</response>
    /// <response code="422">If the request is invalid (validation error).</response>
    /// <response code="409">Conflict if credentials causes a conflict.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        var command = _mapper.Map<RegisterUserCommand>(request);
        var result = await _sender.Send(command);
        return result.IsFailure
            ? result.ToConflictProblemDetails()
            : Created();
    }
}