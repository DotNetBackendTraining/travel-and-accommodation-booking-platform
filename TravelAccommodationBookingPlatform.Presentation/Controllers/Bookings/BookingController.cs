using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Application.Bookings.Commands.CreateBooking;
using TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingDetails;
using TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingDetails.DTOs;
using TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingPayment;
using TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingSearch;
using TravelAccommodationBookingPlatform.Presentation.Attributes;
using TravelAccommodationBookingPlatform.Presentation.Controllers.Bookings.Requests;
using TravelAccommodationBookingPlatform.Presentation.Shared;
using TravelAccommodationBookingPlatform.Presentation.Shared.ResultExtensions;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers.Bookings;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/bookings")]
[RoleAuthorize]
public class BookingController : AbstractController
{
    public BookingController(ISender sender, IMapper mapper) : base(sender, mapper)
    {
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

        var query = Mapper.Map<BookingSearchQuery>(request);
        query.UserId = userIdResult.Value;

        return await HandleQueryResult(query, cancellationToken);
    }

    /// <summary>
    /// Retrieves the details of a specific booking for the user.
    /// </summary>
    /// <param name="id">The unique identifier of the booking.</param>
    /// <param name="parameters">The request containing options of the booking details to return.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The details of the requested booking.</returns>
    /// <response code="200">Returns the details of the booking.</response>
    /// <response code="401">Unauthorized if credentials are invalid.</response>
    /// <response code="404">If the booking is not found for the user.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BookingDetailsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookingDetailsResponse>> GetBookingDetails(
        [FromRoute] Guid id,
        [FromQuery] BookingDetailsParameters parameters,
        CancellationToken cancellationToken)
    {
        var userIdResult = GetUserIdOrFailure();
        if (userIdResult.IsFailure)
        {
            return userIdResult.ToProblemDetails();
        }

        var query = new BookingDetailsQuery
        {
            Id = id,
            UserId = userIdResult.Value,
            Parameters = parameters
        };

        return await HandleQueryResult(query, cancellationToken);
    }

    /// <summary>
    /// Retrieves the details of a specific booking payment for the user.
    /// </summary>
    /// <param name="id">The unique identifier of the booking.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The details of the requested payment.</returns>
    /// <response code="200">Returns the details of the payment.</response>
    /// <response code="401">Unauthorized if credentials are invalid.</response>
    /// <response code="404">If a resource is not found (e.g. Booking, Payment).</response>
    [HttpGet("{id:guid}/payment")]
    [ProducesResponseType(typeof(BookingPaymentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookingPaymentResponse>> GetBookingPaymentDetails(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var userIdResult = GetUserIdOrFailure();
        if (userIdResult.IsFailure)
        {
            return userIdResult.ToProblemDetails();
        }

        var query = new BookingPaymentQuery
        {
            BookingId = id,
            UserId = userIdResult.Value
        };

        return await HandleQueryResult(query, cancellationToken);
    }

    /// <summary>
    /// Creates a new booking for the user.
    /// </summary>
    /// <param name="request">The request containing the details of the booking to create.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The ID of the newly created booking.</returns>
    /// <response code="201">Returns the ID of the newly created booking.</response>
    /// <response code="400">If the request is invalid (input format error).</response>
    /// <response code="401">Unauthorized if credentials are invalid.</response>
    /// <response code="404">If a required resource is not found (e.g. Room).</response>
    /// <response code="404">If the booking cannot be created due to conflicts
    /// (e.g. Rooms from different hotels, Number of guests exceeds the limit, Rooms not available).</response>
    /// <response code="422">If the request is invalid (validation error).</response>
    [HttpPost]
    [ProducesResponseType(typeof(CreateBookingResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<CreateBookingResponse>> CreateBooking(
        [FromForm] CreateBookingRequest request,
        CancellationToken cancellationToken)
    {
        var userIdResult = GetUserIdOrFailure();
        if (userIdResult.IsFailure)
        {
            return userIdResult.ToProblemDetails();
        }

        var command = Mapper.Map<CreateBookingCommand>(request);
        command.UserId = userIdResult.Value;

        var result = await Sender.Send(command, cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction("GetBookingDetails", "Booking", new { id = result.Value.Id }, result.Value)
            : result.ToProblemDetails();
    }
}