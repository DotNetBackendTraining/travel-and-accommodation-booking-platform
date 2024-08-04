using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.FeaturedHotels;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch;
using TravelAccommodationBookingPlatform.Presentation.Hotels.ViewModels;
using TravelAccommodationBookingPlatform.Presentation.Shared;
using TravelAccommodationBookingPlatform.Presentation.Shared.ResultExtensions;

namespace TravelAccommodationBookingPlatform.Presentation.Hotels;

[ApiController]
[Route("api/hotels")]
public class HotelSearchController : AbstractController
{
    private readonly IMapper _mapper;

    public HotelSearchController(
        ISender sender,
        IMapper mapper)
        : base(sender)
    {
        _mapper = mapper;
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
    [HttpGet("search")]
    [ProducesResponseType(typeof(HotelSearchResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<HotelSearchViewModel>> SearchHotels(
        [FromQuery] HotelSearchQuery query,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        return result.IsSuccess
            ? Ok(_mapper.Map<HotelSearchViewModel>(result.Value))
            : result.ToProblemDetails();
    }

    /// <summary>
    /// Searches for featured hotels based on provided filters.
    /// </summary>
    /// <param name="query">The search query with filters.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The search results for featured hotels.</returns>
    /// <response code="200">Returns the search results for featured hotels.</response>
    /// <response code="400">If the request is invalid.</response>
    /// <response code="401">Unauthorized if credentials are invalid.</response>
    /// <response code="422">If the request is invalid (validation error).</response>
    [HttpGet("featured")]
    [ProducesResponseType(typeof(FeaturedHotelsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<FeaturedHotelsViewModel>> FeaturedHotels(
        [FromQuery] FeaturedHotelsQuery query,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        return result.IsSuccess
            ? Ok(_mapper.Map<FeaturedHotelsViewModel>(result.Value))
            : result.ToProblemDetails();
    }
}