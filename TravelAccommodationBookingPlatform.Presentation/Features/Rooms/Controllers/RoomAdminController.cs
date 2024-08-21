using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Application.Features.Rooms.Commands.CreateRoom;
using TravelAccommodationBookingPlatform.Application.Features.Rooms.Commands.DeleteRoom;
using TravelAccommodationBookingPlatform.Application.Features.Rooms.Commands.PatchRoom;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Presentation.Attributes;
using TravelAccommodationBookingPlatform.Presentation.Features.Rooms.Requests;
using TravelAccommodationBookingPlatform.Presentation.Shared;
using TravelAccommodationBookingPlatform.Presentation.Shared.ResultExtensions;
using TravelAccommodationBookingPlatform.Presentation.Shared.Wrappers;

namespace TravelAccommodationBookingPlatform.Presentation.Features.Rooms.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/rooms")]
[RoleAuthorize(UserRole.Admin)]
public class RoomAdminController : AbstractController
{
    public RoomAdminController(ISender sender, IMapper mapper) : base(sender, mapper)
    {
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
    [ProducesError(StatusCodes.Status400BadRequest)]
    [ProducesError(StatusCodes.Status401Unauthorized)]
    [ProducesError(StatusCodes.Status403Forbidden)]
    [ProducesError(StatusCodes.Status404NotFound)]
    [ProducesError(StatusCodes.Status409Conflict)]
    [ProducesError(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<CreateRoomResponse>> CreateRoom(
        [FromForm] CreateRoomRequest request,
        CancellationToken cancellationToken)
    {
        var command = Mapper.Map<CreateRoomCommand>(request);
        var result = await Sender.Send(command, cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction("GetRoomDetails", "Room", new { id = result.Value.Id }, result.Value)
            : result.ToProblemDetails();
    }

    /// <summary>
    /// Updates an existing room.
    /// </summary>
    /// <param name="id">The ID of the room to update.</param>
    /// <param name="patchDoc">The JSON Patch document containing the updates.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>No content if the update is successful.</returns>
    /// <response code="204">No content if the update is successful.</response>
    /// <response code="400">If the request is invalid (input format error).</response>
    /// <response code="401">Unauthorized if credentials are invalid.</response>
    /// <response code="403">Forbidden if user is not an admin.</response>
    /// <response code="404">If a resource is not found (e.g. Room, Hotel).</response>
    /// <response code="422">If the request is invalid (validation error).</response>
    [HttpPatch("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesError(StatusCodes.Status400BadRequest)]
    [ProducesError(StatusCodes.Status401Unauthorized)]
    [ProducesError(StatusCodes.Status403Forbidden)]
    [ProducesError(StatusCodes.Status404NotFound)]
    [ProducesError(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> PatchRoom(
        Guid id,
        [FromBody] JsonPatchDocument<PatchRoomModel> patchDoc,
        CancellationToken cancellationToken)
    {
        var command = new PatchRoomCommand
        {
            Id = id,
            PatchDocument = new JsonPatchDocumentWrapper<PatchRoomModel>(patchDoc)
        };
        return await HandleNoContentCommand(command, cancellationToken);
    }

    /// <summary>
    /// Deletes an existing room.
    /// </summary>
    /// <param name="id">The ID of the room to delete.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>No content if the delete is successful.</returns>
    /// <response code="204">No content if the deletion is successful.</response>
    /// <response code="401">Unauthorized if credentials are invalid.</response>
    /// <response code="403">Forbidden if user is not an admin.</response>
    /// <response code="404">If the room is not found.</response>
    /// <response code="409">If the room cannot be deleted due to conflicts (e.g. existing bookings).</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesError(StatusCodes.Status401Unauthorized)]
    [ProducesError(StatusCodes.Status403Forbidden)]
    [ProducesError(StatusCodes.Status404NotFound)]
    [ProducesError(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> DeleteRoom(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteRoomCommand { Id = id };
        return await HandleNoContentCommand(command, cancellationToken);
    }
}