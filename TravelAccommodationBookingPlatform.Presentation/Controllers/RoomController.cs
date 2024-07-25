using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Application.Rooms.Queries.RoomDetails;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers;

[Authorize]
[ApiController]
[Route("api/rooms/{id:guid}")]
public class RoomController : AbstractController
{
    public RoomController(ISender sender) : base(sender)
    {
    }

    /// <summary>
    /// Retrieves the details of a specific room.
    /// </summary>
    /// <param name="id">The unique identifier of the room.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The details of the requested room.</returns>
    /// <response code="200">Returns the details of the room.</response>
    /// <response code="401">Unauthorized if credentials are invalid.</response>
    /// <response code="404">If the room is not found.</response>
    [HttpGet]
    [ProducesResponseType(typeof(RoomDetailsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RoomDetailsResponse>> GetRoomDetails(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var query = new RoomDetailsQuery { Id = id };
        return await HandleQueryResult(query, cancellationToken);
    }
}