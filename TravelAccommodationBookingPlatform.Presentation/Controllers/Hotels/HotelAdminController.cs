using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Application.Hotels.Commands.CreateHotel;
using TravelAccommodationBookingPlatform.Application.Hotels.Commands.DeleteHotel;
using TravelAccommodationBookingPlatform.Application.Hotels.Commands.PatchHotel;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Presentation.Attributes;
using TravelAccommodationBookingPlatform.Presentation.Controllers.Hotels.Requests;
using TravelAccommodationBookingPlatform.Presentation.Shared;
using TravelAccommodationBookingPlatform.Presentation.Shared.ResultExtensions;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers.Hotels;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/hotels")]
[RoleAuthorize(UserRole.Admin)]
public class HotelAdminController : AbstractController
{
    public HotelAdminController(ISender sender, IMapper mapper) : base(sender, mapper)
    {
    }

    /// <summary>
    /// Creates a new hotel.
    /// </summary>
    /// <param name="request">The request containing the details of the hotel to create.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The ID of the newly created hotel.</returns>
    /// <response code="201">Returns the ID of the newly created hotel.</response>
    /// <response code="400">If the request is invalid (input format error).</response>
    /// <response code="401">Unauthorized if credentials are invalid.</response>
    /// <response code="403">Forbidden if user is not an admin.</response>
    /// <response code="404">If a required resource is not found (e.g. City).</response>
    /// <response code="422">If the request is invalid (validation error).</response>
    [HttpPost]
    [ProducesResponseType(typeof(CreateHotelResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<CreateHotelResponse>> CreateHotel(
        [FromForm] CreateHotelRequest request,
        CancellationToken cancellationToken)
    {
        var command = Mapper.Map<CreateHotelCommand>(request);
        var result = await Sender.Send(command, cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction("GetHotelDetails", "Hotel", new { id = result.Value.Id }, result.Value)
            : result.ToProblemDetails();
    }

    /// <summary>
    /// Updates an existing hotel.
    /// </summary>
    /// <param name="id">The ID of the hotel to update.</param>
    /// <param name="patchDoc">The JSON Patch document containing the updates.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>No content if the update is successful.</returns>
    /// <response code="204">No content if the update is successful.</response>
    /// <response code="400">If the request is invalid (input format error).</response>
    /// <response code="401">Unauthorized if credentials are invalid.</response>
    /// <response code="403">Forbidden if user is not an admin.</response>
    /// <response code="404">If a resource is not found (e.g. Hotel, City).</response>
    /// <response code="422">If the request is invalid (validation error).</response>
    [HttpPatch("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> PatchHotel(
        Guid id,
        [FromBody] JsonPatchDocument<PatchHotelModel> patchDoc,
        CancellationToken cancellationToken)
    {
        var command = new PatchHotelCommand
        {
            Id = id,
            PatchDocument = new JsonPatchDocumentWrapper<PatchHotelModel>(patchDoc)
        };
        return await HandleNoContentCommand(command, cancellationToken);
    }

    /// <summary>
    /// Deletes an existing hotel.
    /// </summary>
    /// <param name="id">The ID of the hotel to delete.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>No content if the delete is successful.</returns>
    /// <response code="204">No content if the deletion is successful.</response>
    /// <response code="401">Unauthorized if credentials are invalid.</response>
    /// <response code="403">Forbidden if user is not an admin.</response>
    /// <response code="404">If the hotel is not found.</response>
    /// <response code="409">If the hotel cannot be deleted due to conflicts (e.g. existing bookings).</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> DeleteHotel(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteHotelCommand { Id = id };
        return await HandleNoContentCommand(command, cancellationToken);
    }
}