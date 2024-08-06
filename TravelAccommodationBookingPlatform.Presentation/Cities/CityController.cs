using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Application.Cities.Queries.CityDetails;
using TravelAccommodationBookingPlatform.Application.Cities.Queries.CitySearch;
using TravelAccommodationBookingPlatform.Presentation.Attributes;
using TravelAccommodationBookingPlatform.Presentation.Shared;

namespace TravelAccommodationBookingPlatform.Presentation.Cities;

[ApiController]
[Route("api/cities")]
[RoleAuthorize]
public class CityController : AbstractController
{
    public CityController(ISender sender) : base(sender)
    {
    }

    /// <summary>
    /// Retrieves the details of a specific city.
    /// </summary>
    /// <param name="id">The unique identifier of the city.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The details of the requested room.</returns>
    /// <response code="200">Returns the details of the city.</response>
    /// <response code="401">Unauthorized if credentials are invalid.</response>
    /// <response code="404">If the city is not found.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CityDetailsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CityDetailsResponse>> GetCityDetails(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var query = new CityDetailsQuery { Id = id };
        return await HandleQueryResult(query, cancellationToken);
    }


    /// <summary>
    /// Searches for cities based on provided filters and returns available search filters.
    /// </summary>
    /// <param name="query">The search query with filters.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The search results and available filters.</returns>
    /// <response code="200">Returns the search results and filters.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="401">Unauthorized if credentials are invalid.</response>
    /// <response code="422">If the request is invalid (validation error).</response>
    [HttpGet]
    [ProducesResponseType(typeof(CitySearchResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<CitySearchResponse>> CitySearch(
        [FromQuery] CitySearchQuery query,
        CancellationToken cancellationToken)
    {
        return await HandleQueryResult(query, cancellationToken);
    }
}