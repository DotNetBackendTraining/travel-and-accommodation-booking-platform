using Microsoft.AspNetCore.Http;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers.ResultExtensions;

public static class ProblemDetailsExtension
{
    public static IResult ToProblemDetails(this Result result)
    {
        return CreateProblemDetails(
            result,
            StatusCodes.Status400BadRequest,
            "Bad Request",
            "https://tools.ietf.org/html/rfc7231#section-6.5.1");
    }

    public static IResult ToNotFoundProblemDetails(this Result result)
    {
        return CreateProblemDetails(
            result,
            StatusCodes.Status404NotFound,
            "Not Found",
            "https://tools.ietf.org/html/rfc7231#section-6.5.4");
    }

    public static IResult ToUnauthorizedProblemDetails(this Result result)
    {
        return CreateProblemDetails(
            result,
            StatusCodes.Status401Unauthorized,
            "Unauthorized",
            "https://tools.ietf.org/html/rfc7235#section-3.1");
    }

    public static IResult ToForbiddenProblemDetails(this Result result)
    {
        return CreateProblemDetails(
            result,
            StatusCodes.Status403Forbidden,
            "Forbidden",
            "https://tools.ietf.org/html/rfc7231#section-6.5.3");
    }

    public static IResult ToConflictProblemDetails(this Result result)
    {
        return CreateProblemDetails(
            result,
            StatusCodes.Status409Conflict,
            "Conflict",
            "https://tools.ietf.org/html/rfc7231#section-6.5.8");
    }

    public static IResult ToInternalServerErrorProblemDetails(this Result result)
    {
        return CreateProblemDetails(
            result,
            StatusCodes.Status500InternalServerError,
            "Internal Server Error",
            "https://tools.ietf.org/html/rfc7231#section-6.6.1");
    }

    private static IResult CreateProblemDetails(Result result, int statusCode, string title, string type)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException($"Cannot call ${CreateProblemDetails} on successful result");
        }

        return Results.Problem(
            statusCode: statusCode,
            title: title,
            type: type,
            extensions: new Dictionary<string, object>
            {
                { "errors", new[] { result.Error } }
            }!);
    }
}