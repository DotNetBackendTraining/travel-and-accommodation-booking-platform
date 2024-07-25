using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelDetails;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelImages;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelReviews;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelRooms;
using TravelAccommodationBookingPlatform.Application.Shared.Pagination;
using TravelAccommodationBookingPlatform.Domain.Enums;

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

    /// <summary>
    /// Retrieves the images of a specific hotel.
    /// </summary>
    /// <param name="id">The unique identifier of the hotel.</param>
    /// <param name="paginationParameters">Pagination parameters for the request.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The images of the requested hotel.</returns>
    /// <response code="200">Returns the images of the hotel.</response>
    /// <response code="401">Unauthorized if credentials are invalid.</response>
    /// <response code="404">If the hotel is not found.</response>
    /// <response code="422">If the request is invalid (validation error).</response>
    [HttpGet("images")]
    [ProducesResponseType(typeof(HotelImagesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<HotelImagesResponse>> GetHotelImages(
        [FromRoute] Guid id,
        [FromQuery] PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        var query = new HotelImagesQuery { Id = id, PaginationParameters = paginationParameters };
        return await HandleQueryResult(query, cancellationToken);
    }

    /// <summary>
    /// Retrieves the reviews of a specific hotel.
    /// </summary>
    /// <param name="id">The unique identifier of the hotel.</param>
    /// <param name="paginationParameters">Pagination parameters for the request.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The reviews of the requested hotel.</returns>
    /// <response code="200">Returns the reviews of the hotel.</response>
    /// <response code="401">Unauthorized if credentials are invalid.</response>
    /// <response code="404">If the hotel is not found.</response>
    /// <response code="422">If the request is invalid (validation error).</response>
    [HttpGet("reviews")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<HotelReviewsResponse>> GetHotelReviews(
        [FromRoute] Guid id,
        [FromQuery] PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        var query = new HotelReviewsQuery { Id = id, PaginationParameters = paginationParameters };
        return await HandleQueryResult(query, cancellationToken);
    }

    /// <summary>
    /// Retrieves the rooms of a specific hotel.
    /// </summary>
    /// <param name="id">The unique identifier of the hotel.</param>
    /// <param name="paginationParameters">Pagination parameters for the request.</param>
    /// <param name="roomType">The type of the room.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The rooms of the requested hotel.</returns>
    /// <response code="200">Returns the rooms of the hotel.</response>
    /// <response code="401">Unauthorized if credentials are invalid.</response>
    /// <response code="404">If the hotel is not found.</response>
    /// <response code="422">If the request is invalid (validation error).</response>
    [HttpGet("rooms")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<HotelRoomsResponse>> GetHotelRooms(
        [FromRoute] Guid id,
        [FromQuery] PaginationParameters paginationParameters,
        [FromQuery] RoomType? roomType,
        CancellationToken cancellationToken)
    {
        var query = new HotelRoomsQuery
        {
            Id = id,
            PaginationParameters = paginationParameters,
            RoomType = roomType
        };
        return await HandleQueryResult(query, cancellationToken);
    }
}