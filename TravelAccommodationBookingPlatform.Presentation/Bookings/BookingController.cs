using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingSearch;
using TravelAccommodationBookingPlatform.Presentation.Attributes;
using TravelAccommodationBookingPlatform.Presentation.Bookings.Requests;
using TravelAccommodationBookingPlatform.Presentation.Shared;
using TravelAccommodationBookingPlatform.Presentation.Shared.ResultExtensions;

namespace TravelAccommodationBookingPlatform.Presentation.Bookings;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/bookings")]
[RoleAuthorize]
public class BookingController : AbstractController
{
    private readonly IMapper _mapper;

    public BookingController(ISender sender, IMapper mapper) : base(sender)
    {
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves bookings for the user with various filtering options.
    /// </summary>
    /// <param name="request">The bookings request with filters.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The bookings for the authenticated user.</returns>
    /// <response code="200">Returns the bookings for the authenticated user.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="401">Unauthorized if credentials are invalid.</response>
    /// <response code="422">If the request is invalid (validation error).</response>
    [HttpGet]
    [ProducesResponseType(typeof(BookingSearchResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<BookingSearchResponse>> BookingSearch(
        [FromQuery] BookingSearchRequest request,
        CancellationToken cancellationToken)
    {
        var userIdResult = GetUserIdOrFailure();
        if (userIdResult.IsFailure)
        {
            return userIdResult.ToProblemDetails();
        }

        var query = _mapper.Map<BookingSearchQuery>(request);
        query.UserId = userIdResult.Value;

        return await HandleQueryResult(query, cancellationToken);
    }
}