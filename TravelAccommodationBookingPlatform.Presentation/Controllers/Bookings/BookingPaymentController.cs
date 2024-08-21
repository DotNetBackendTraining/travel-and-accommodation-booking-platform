using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Application.Features.Bookings.Queries.BookingPayment;
using TravelAccommodationBookingPlatform.Presentation.Attributes;
using TravelAccommodationBookingPlatform.Presentation.Shared;
using TravelAccommodationBookingPlatform.Presentation.Shared.ResultExtensions;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers.Bookings;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/bookings/{id:guid}/payment")]
[RoleAuthorize]
public class BookingPaymentController : AbstractController
{
    public BookingPaymentController(ISender sender, IMapper mapper) : base(sender, mapper)
    {
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
    [HttpGet]
    [ProducesResponseType(typeof(BookingPaymentResponse), StatusCodes.Status200OK)]
    [ProducesError(StatusCodes.Status401Unauthorized)]
    [ProducesError(StatusCodes.Status404NotFound)]
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
}