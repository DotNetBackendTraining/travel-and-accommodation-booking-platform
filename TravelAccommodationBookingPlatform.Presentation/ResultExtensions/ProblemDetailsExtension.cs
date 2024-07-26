using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Presentation.ResultExtensions;

public static class ProblemDetailsExtension
{
    public static ObjectResult ToProblemDetails(this Result result)
    {
        return result.Error.Type switch
        {
            ErrorType.BadRequest => result.ToBadRequestProblemDetails(),
            ErrorType.NotFound => result.ToNotFoundProblemDetails(),
            ErrorType.NotAuthorized => result.ToUnauthorizedProblemDetails(),
            ErrorType.Conflict => result.ToConflictProblemDetails(),
            ErrorType.InternalServerError => result.ToInternalServerErrorProblemDetails(),
            ErrorType.None => throw new InvalidOperationException(
                $"Cannot create problem details for the successful result '{result}'"),
            _ => throw new ArgumentOutOfRangeException(nameof(result.Error.Type) + " undefined type")
        };
    }

    private static BadRequestObjectResult ToBadRequestProblemDetails(this Result result)
    {
        return new BadRequestObjectResult(CreateProblemDetails(
            result,
            StatusCodes.Status400BadRequest,
            "Bad Request",
            "https://tools.ietf.org/html/rfc7231#section-6.5.1"));
    }

    private static NotFoundObjectResult ToNotFoundProblemDetails(this Result result)
    {
        return new NotFoundObjectResult(CreateProblemDetails(
            result,
            StatusCodes.Status404NotFound,
            "Not Found",
            "https://tools.ietf.org/html/rfc7231#section-6.5.4"));
    }

    private static UnauthorizedObjectResult ToUnauthorizedProblemDetails(this Result result)
    {
        return new UnauthorizedObjectResult(CreateProblemDetails(
            result,
            StatusCodes.Status401Unauthorized,
            "Unauthorized",
            "https://tools.ietf.org/html/rfc7235#section-3.1"));
    }

    private static ConflictObjectResult ToConflictProblemDetails(this Result result)
    {
        return new ConflictObjectResult(CreateProblemDetails(
            result,
            StatusCodes.Status409Conflict,
            "Conflict",
            "https://tools.ietf.org/html/rfc7231#section-6.5.8"));
    }

    private static ObjectResult ToInternalServerErrorProblemDetails(this Result result)
    {
        return new ObjectResult(CreateProblemDetails(
            result,
            StatusCodes.Status500InternalServerError,
            "Internal Server Error",
            "https://tools.ietf.org/html/rfc7231#section-6.6.1"))
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }

    private static ProblemDetails CreateProblemDetails(
        Result result,
        int statusCode,
        string title,
        string type)
    {
        return new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Extensions =
            {
                ["errors"] = new[]
                {
                    new
                    {
                        code = result.Error.Code,
                        message = result.Error.Message
                    }
                }
            }
        };
    }
}