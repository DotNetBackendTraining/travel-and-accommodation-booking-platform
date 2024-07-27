using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Presentation.Shared.ResultExtensions;

namespace TravelAccommodationBookingPlatform.Presentation.Shared;

public abstract class AbstractController : ControllerBase
{
    protected readonly ISender Sender;

    protected AbstractController(ISender sender)
    {
        Sender = sender;
    }

    protected async Task<ActionResult<TResponse>> HandleQueryResult<TResponse>(
        IQuery<TResponse> query,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(query, cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value)
            : result.ToProblemDetails();
    }
}