using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelDetails;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers;

[Authorize]
[ApiController]
[Route("api/hotels/{id:guid}")]
public class HotelController : AbstractController
{
    public HotelController(ISender sender) : base(sender)
    {
    }

    /// <summary>
    /// Retrieves the details of a specific hotel.
    /// </summary>
    /// <param name="id">The unique identifier of the hotel.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The details of the requested hotel.</returns>
    /// <response code="200">Returns the details of the hotel.</response>
    /// <response code="401">Unauthorized if credentials are invalid.</response>
    /// <response code="404">If the hotel is not found.</response>
    [HttpGet]
    [ProducesResponseType(typeof(HotelDetailsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HotelDetailsResponse>> GetHotelDetails(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var query = new HotelDetailsQuery { Id = id };
        return await HandleQueryResult(query, cancellationToken);
    }
}