using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.Shared;
using TravelAccommodationBookingPlatform.Presentation.Shared.ResultExtensions;

namespace TravelAccommodationBookingPlatform.Presentation.Attributes;

public class RoleAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    private readonly UserRole[] _roles;

    public RoleAuthorizeAttribute() : this(UserRole.User, UserRole.Admin)
    {
    }

    public RoleAuthorizeAttribute(params UserRole[] roles)
    {
        _roles = roles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (user.Identity is { IsAuthenticated: false })
        {
            context.Result = Result
                .Failure(DomainErrors.User.CredentialsNotProvided)
                .ToProblemDetails();
            return;
        }

        var userRoles = user.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => Enum.Parse<UserRole>(c.Value))
            .ToList();

        if (!_roles.Any(role => userRoles.Contains(role)))
        {
            context.Result = Result
                .Failure(DomainErrors.User.InvalidRole)
                .ToProblemDetails();
        }
    }
}