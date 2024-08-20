using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Application.Cities.Queries.CityDetails;
using TravelAccommodationBookingPlatform.Application.Cities.Queries.CityDetails.Admin;
using TravelAccommodationBookingPlatform.Application.Cities.Queries.CitySearch;
using TravelAccommodationBookingPlatform.Presentation.Attributes;
using TravelAccommodationBookingPlatform.Presentation.Controllers.Cities.ViewModels;
using TravelAccommodationBookingPlatform.Presentation.Shared;
using TravelAccommodationBookingPlatform.Presentation.Shared.ResultExtensions;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers.Cities;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/cities")]
[RoleAuthorize]
public class CityController : AbstractController
{
    public CityController(ISender sender, IMapper mapper) : base(sender, mapper)
    {
    }

    /// <summary>
    /// Retrieves the details of a specific city.
    /// </summary>
    /// <param name="id">The unique identifier of the city.</param>
    /// <param name="cancellationToken">Cancellation token for the request.</param>
    /// <returns>The details of the requested city.</returns>
    /// <response code="200">Returns the details of the city.</response>
    /// <response code="401">Unauthorized if credentials are invalid.</response>
    /// <response code="404">If the city is not found.</response>
    [HttpGet("{id:guid}")]
    [MultipleResponseTypes(StatusCodes.Status200OK,
        typeof(CityDetailsResponse),
        typeof(AdminCityDetailsResponse))]
    [ProducesError(StatusCodes.Status401Unauthorized)]
    [ProducesError(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CityDetailsResponse>> GetCityDetails(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        return await HandleUserRoleResult(
            adminResultFactory: async () =>
            {
                var query = new AdminCityDetailsQuery { Id = id };
                var result = await Sender.Send(query, cancellationToken);
                return result.IsSuccess
                    ? Ok(Mapper.Map<AdminCityDetailsViewModel>(result.Value))
                    : result.ToProblemDetails();
            },
            userResultFactory: async () =>
            {
                var query = new CityDetailsQuery { Id = id };
                var result = await Sender.Send(query, cancellationToken);
                return result.IsSuccess
                    ? Ok(Mapper.Map<CityDetailsViewModel>(result.Value))
                    : result.ToProblemDetails();
            });
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
    [ProducesError(StatusCodes.Status400BadRequest)]
    [ProducesError(StatusCodes.Status401Unauthorized)]
    [ProducesError(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<CitySearchResponse>> CitySearch(
        [FromQuery] CitySearchQuery query,
        CancellationToken cancellationToken)
    {
        return await HandleQueryResult(query, cancellationToken);
    }
}