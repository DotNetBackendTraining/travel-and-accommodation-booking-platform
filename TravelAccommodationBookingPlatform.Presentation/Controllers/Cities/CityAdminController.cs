using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Application.Cities.Commands.CreateCity;
using TravelAccommodationBookingPlatform.Application.Cities.Commands.DeleteCity;
using TravelAccommodationBookingPlatform.Application.Cities.Commands.PatchCity;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Presentation.Attributes;
using TravelAccommodationBookingPlatform.Presentation.Controllers.Cities.Requests;
using TravelAccommodationBookingPlatform.Presentation.Shared;
using TravelAccommodationBookingPlatform.Presentation.Shared.ResultExtensions;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers.Cities;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/cities")]
[RoleAuthorize(UserRole.Admin)]
public class CityAdminController : AbstractController
{
    private readonly IMapper _mapper;

    public CityAdminController(
        ISender sender,
        IMapper mapper)
        : base(sender)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new city.
    /// </summary>
    /// <param name="request">The request containing the details of the city to create.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The ID of the newly created city.</returns>
    /// <response code="201">Returns the ID of the newly created city.</response>
    /// <response code="400">If the request is invalid (input format error).</response>
    /// <response code="401">Unauthorized if credentials are invalid.</response>
    /// <response code="403">Forbidden if user is not an admin.</response>
    /// <response code="422">If the request is invalid (validation error).</response>
    [HttpPost]
    [ProducesResponseType(typeof(CreateCityResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<CreateCityResponse>> CreateCity(
        [FromForm] CreateCityRequest request,
        CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateCityCommand>(request);
        var result = await Sender.Send(command, cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction("GetCityDetails", "City", new { id = result.Value.Id }, result.Value)
            : result.ToProblemDetails();
    }

    /// <summary>
    /// Updates an existing city.
    /// </summary>
    /// <param name="id">The ID of the city to update.</param>
    /// <param name="patchDoc">The JSON Patch document containing the updates.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>No content if the update is successful.</returns>
    /// <response code="204">No content if the update is successful.</response>
    /// <response code="400">If the request is invalid (input format error).</response>
    /// <response code="401">Unauthorized if credentials are invalid.</response>
    /// <response code="403">Forbidden if user is not an admin.</response>
    /// <response code="404">If a resource is not found (e.g. City).</response>
    /// <response code="422">If the request is invalid (validation error).</response>
    [HttpPatch("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> PatchCity(
        Guid id,
        [FromBody] JsonPatchDocument<PatchCityModel> patchDoc,
        CancellationToken cancellationToken)
    {
        var command = new PatchCityCommand
        {
            Id = id,
            PatchDocument = new JsonPatchDocumentWrapper<PatchCityModel>(patchDoc)
        };
        return await HandleNoContentCommand(command, cancellationToken);
    }


    /// <summary>
    /// Deletes an existing city.
    /// </summary>
    /// <param name="id">The ID of the city to delete.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>No content if the delete is successful.</returns>
    /// <response code="204">No content if the deletion is successful.</response>
    /// <response code="401">Unauthorized if credentials are invalid.</response>
    /// <response code="403">Forbidden if user is not an admin.</response>
    /// <response code="404">If the city is not found.</response>
    /// <response code="409">If the city cannot be deleted due to conflicts (e.g. existing hotels).</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> DeleteCity(
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteCityCommand { Id = id };
        return await HandleNoContentCommand(command, cancellationToken);
    }
}