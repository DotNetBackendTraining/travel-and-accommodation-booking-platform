using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Application.Cities.Queries.CityDetails;
using TravelAccommodationBookingPlatform.Presentation.Shared;

namespace TravelAccommodationBookingPlatform.Presentation.Cities;

[Authorize]
[ApiController]
[Route("api/cities")]
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
}