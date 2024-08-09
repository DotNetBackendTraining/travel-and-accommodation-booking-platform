using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Application.Cities.Commands.CreateCity;
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
}