using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Shared;
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

    protected async Task<ActionResult> HandleNoContentCommand(
        ICommand command,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);
        return result.IsSuccess
            ? NoContent()
            : result.ToProblemDetails();
    }

    protected Result<Guid> GetUserIdOrFailure()
    {
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        return userIdClaim is null
            ? Result.Failure<Guid>(DomainErrors.User.CredentialsNotProvided)
            : Result.Success(Guid.Parse(userIdClaim.Value));
    }
}