using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers.ResultExtensions;

public static class ProblemDetailsExtension
{
    public static BadRequestObjectResult ToProblemDetails(this Result result)
    {
        return new BadRequestObjectResult(CreateProblemDetails(
            result,
            StatusCodes.Status400BadRequest,
            "Bad Request",
            "https://tools.ietf.org/html/rfc7231#section-6.5.1"));
    }

    public static NotFoundObjectResult ToNotFoundProblemDetails(this Result result)
    {
        return new NotFoundObjectResult(CreateProblemDetails(
            result,
            StatusCodes.Status404NotFound,
            "Not Found",
            "https://tools.ietf.org/html/rfc7231#section-6.5.4"));
    }

    public static UnauthorizedObjectResult ToUnauthorizedProblemDetails(this Result result)
    {
        return new UnauthorizedObjectResult(CreateProblemDetails(
            result,
            StatusCodes.Status401Unauthorized,
            "Unauthorized",
            "https://tools.ietf.org/html/rfc7235#section-3.1"));
    }

    public static ConflictObjectResult ToConflictProblemDetails(this Result result)
    {
        return new ConflictObjectResult(CreateProblemDetails(
            result,
            StatusCodes.Status409Conflict,
            "Conflict",
            "https://tools.ietf.org/html/rfc7231#section-6.5.8"));
    }

    private static IResult CreateProblemDetails(
        Result result,
        int statusCode,
        string title,
        string type)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException($"Cannot create problem details for the successful result '{result}'");
        }

        return Results.Problem(
            statusCode: statusCode,
            title: title,
            type: type,
            extensions: new Dictionary<string, object>
                { { "errors", new[] { result.Error } } }!);
    }
}