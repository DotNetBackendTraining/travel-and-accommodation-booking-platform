using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Application.Users.Commands.LoginUser;
using TravelAccommodationBookingPlatform.Application.Users.Commands.RegisterUser;
using TravelAccommodationBookingPlatform.Presentation.Shared.ResultExtensions;
using TravelAccommodationBookingPlatform.Presentation.Users.Requests;

namespace TravelAccommodationBookingPlatform.Presentation.Users;

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
    /// <param name="cancellationToken">Cancellation token for the request</param>
    /// <returns>A token if login is successful; otherwise, an error.</returns>
    /// <response code="200">Returns the newly created token.</response>
    /// <response code="422">If the request is invalid (validation error).</response>
    /// <response code="401">Unauthorized if credentials are incorrect.</response>
    [HttpPost("token")]
    [ProducesResponseType(typeof(LoginUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<LoginUserResponse>> Login(
        [FromBody] LoginUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<LoginUserCommand>(request);
        var result = await _sender.Send(command, cancellationToken);
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
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<RegisterUserCommand>(request);
        var result = await _sender.Send(command, cancellationToken);
        return result.IsSuccess
            ? Created()
            : result.ToProblemDetails();
    }
}