using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Application.Rooms.Commands.CreateRoom;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Presentation.Attributes;
using TravelAccommodationBookingPlatform.Presentation.Controllers.Rooms.Requests;
using TravelAccommodationBookingPlatform.Presentation.Shared;
using TravelAccommodationBookingPlatform.Presentation.Shared.ResultExtensions;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers.Rooms;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/rooms")]
[RoleAuthorize(UserRole.Admin)]
public class RoomAdminController : AbstractController
{
    private readonly IMapper _mapper;

    public RoomAdminController(
        ISender sender,
        IMapper mapper)
        : base(sender)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new room.
    /// </summary>
    /// <param name="request">The request containing the details of the room to create.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The ID of the newly created room.</returns>
    /// <response code="201">Returns the ID of the newly created room.</response>
    /// <response code="400">If the request is invalid (input format error).</response>
    /// <response code="401">Unauthorized if credentials are invalid.</response>
    /// <response code="403">Forbidden if user is not an admin.</response>
    /// <response code="404">If a required resource is not found (e.g. Hotel).</response>
    /// <response code="409">If the room cannot be created due to conflicts (e.g. existing room number).</response>
    /// <response code="422">If the request is invalid (validation error).</response>
    [HttpPost]
    [ProducesResponseType(typeof(CreateRoomResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<CreateRoomResponse>> CreateRoom(
        [FromForm] CreateRoomRequest request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateRoomCommand>(request);
        var result = await Sender.Send(command, cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction("GetRoomDetails", "Room", new { id = result.Value.Id }, result.Value)
            : result.ToProblemDetails();
    }
}