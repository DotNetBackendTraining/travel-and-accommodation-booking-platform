using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Application.Rooms.Queries.RoomDetails;
using TravelAccommodationBookingPlatform.Application.Rooms.Queries.RoomDetails.Admin;
using TravelAccommodationBookingPlatform.Application.Rooms.Queries.RoomImages;
using TravelAccommodationBookingPlatform.Application.Shared;
using TravelAccommodationBookingPlatform.Presentation.Attributes;
using TravelAccommodationBookingPlatform.Presentation.Controllers.Rooms.ViewModels;
using TravelAccommodationBookingPlatform.Presentation.Shared;
using TravelAccommodationBookingPlatform.Presentation.Shared.ResultExtensions;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers.Rooms;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/rooms/{id:guid}")]
[RoleAuthorize]
public class RoomController : AbstractController
{
    public RoomController(ISender sender, IMapper mapper) : base(sender, mapper)
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
    [MultipleResponseTypes(StatusCodes.Status200OK,
        typeof(RoomDetailsResponse),
        typeof(AdminRoomDetailsResponse))]
    [ProducesError(StatusCodes.Status401Unauthorized)]
    [ProducesError(StatusCodes.Status404NotFound)]
    public async Task<ObjectResult> GetRoomDetails(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        return await HandleUserRoleResult(
            adminResultFactory: async () =>
            {
                var query = new AdminRoomDetailsQuery { Id = id };
                var result = await Sender.Send(query, cancellationToken);
                return result.IsSuccess
                    ? Ok(Mapper.Map<AdminRoomDetailsViewModel>(result.Value))
                    : result.ToProblemDetails();
            },
            userResultFactory: async () =>
            {
                var query = new RoomDetailsQuery { Id = id };
                var result = await Sender.Send(query, cancellationToken);
                return result.IsSuccess
                    ? Ok(Mapper.Map<RoomDetailsViewModel>(result.Value))
                    : result.ToProblemDetails();
            });
    }

    /// <summary>
    /// Retrieves the images of a specific room.
    /// </summary>
    /// <param name="id">The unique identifier of the room.</param>
    /// <param name="paginationParameters">Pagination parameters for the request.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The images of the requested room.</returns>
    /// <response code="200">Returns the images of the room.</response>
    /// <response code="401">Unauthorized if credentials are invalid.</response>
    /// <response code="422">If the request is invalid (validation error).</response>
    /// <response code="404">If the room is not found.</response>
    [HttpGet("images")]
    [ProducesResponseType(typeof(RoomImagesResponse), StatusCodes.Status200OK)]
    [ProducesError(StatusCodes.Status401Unauthorized)]
    [ProducesError(StatusCodes.Status404NotFound)]
    [ProducesError(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<RoomImagesResponse>> GetHotelImages(
        [FromRoute] Guid id,
        [FromQuery] PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        var query = new RoomImagesQuery { Id = id, PaginationParameters = paginationParameters };
        return await HandleQueryResult(query, cancellationToken);
    }
}