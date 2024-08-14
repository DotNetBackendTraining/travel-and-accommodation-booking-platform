using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelDetails;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelImages;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelReviews;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelRooms;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch;
using TravelAccommodationBookingPlatform.Application.Shared;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Presentation.Attributes;
using TravelAccommodationBookingPlatform.Presentation.Controllers.Hotels.ViewModels;
using TravelAccommodationBookingPlatform.Presentation.Shared;
using TravelAccommodationBookingPlatform.Presentation.Shared.ResultExtensions;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers.Hotels;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/hotels")]
[RoleAuthorize]
public class HotelController : AbstractController
{
    public HotelController(ISender sender, IMapper mapper) : base(sender, mapper)
    {
    }

    /// <summary>
    /// Searches for hotels based on provided filters and returns available search filters.
    /// </summary>
    /// <param name="query">The search query with filters.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The search results and available filters.</returns>
    /// <response code="200">Returns the search results and filters.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="401">Unauthorized if credentials are invalid.</response>
    /// <response code="422">If the request is invalid (validation error).</response>
    [HttpGet]
    [ProducesResponseType(typeof(HotelSearchResponse), StatusCodes.Status200OK)]
    [ProducesError(StatusCodes.Status400BadRequest)]
    [ProducesError(StatusCodes.Status401Unauthorized)]
    [ProducesError(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<HotelSearchViewModel>> HotelSearch(
        [FromQuery] HotelSearchQuery query,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        return result.IsSuccess
            ? Ok(Mapper.Map<HotelSearchViewModel>(result.Value))
            : result.ToProblemDetails();
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
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(HotelDetailsResponse), StatusCodes.Status200OK)]
    [ProducesError(StatusCodes.Status401Unauthorized)]
    [ProducesError(StatusCodes.Status404NotFound)]
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
    [HttpGet("{id:guid}/images")]
    [ProducesResponseType(typeof(HotelImagesResponse), StatusCodes.Status200OK)]
    [ProducesError(StatusCodes.Status401Unauthorized)]
    [ProducesError(StatusCodes.Status404NotFound)]
    [ProducesError(StatusCodes.Status422UnprocessableEntity)]
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
    [HttpGet("{id:guid}/reviews")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesError(StatusCodes.Status401Unauthorized)]
    [ProducesError(StatusCodes.Status404NotFound)]
    [ProducesError(StatusCodes.Status422UnprocessableEntity)]
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
    [HttpGet("{id:guid}/rooms")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesError(StatusCodes.Status401Unauthorized)]
    [ProducesError(StatusCodes.Status404NotFound)]
    [ProducesError(StatusCodes.Status422UnprocessableEntity)]
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