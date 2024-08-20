using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.Shared;
using TravelAccommodationBookingPlatform.Presentation.Constants;
using TravelAccommodationBookingPlatform.Presentation.Shared.ResultExtensions;

namespace TravelAccommodationBookingPlatform.Presentation.Shared;

[EnableRateLimiting(PresentationRules.RateLimitPolicies.Fixed)]
public abstract class AbstractController : ControllerBase
{
    protected readonly ISender Sender;
    protected readonly IMapper Mapper;

    protected AbstractController(ISender sender, IMapper mapper)
    {
        Sender = sender;
        Mapper = mapper;
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

    protected Result<UserRole> GetUserRoleOrFailure()
    {
        var userRoleClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
        return userRoleClaim is null
            ? Result.Failure<UserRole>(DomainErrors.User.CredentialsNotProvided)
            : Result.Success(Enum.Parse<UserRole>(userRoleClaim.Value));
    }

    protected async Task<ObjectResult> HandleUserRoleResult(
        Func<Task<ObjectResult>> adminResultFactory,
        Func<Task<ObjectResult>> userResultFactory)
    {
        var userRoleResult = GetUserRoleOrFailure();
        if (userRoleResult.IsFailure)
        {
            return userRoleResult.ToProblemDetails();
        }

        if (userRoleResult.Value is UserRole.Admin)
        {
            return await adminResultFactory();
        }

        return await userResultFactory();
    }
}