using Microsoft.AspNetCore.JsonPatch.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using TravelAccommodationBookingPlatform.Domain.Shared;
using TravelAccommodationBookingPlatform.Presentation.Shared.ResultExtensions;

namespace TravelAccommodationBookingPlatform.Presentation.Filters;

public class JsonPatchExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not JsonPatchException jsonPatchException)
        {
            return;
        }

        var error = new Error(
            ErrorType.BadRequest,
            "JSON Patch Error",
            jsonPatchException.Message);

        context.Result = Result
            .Failure(error)
            .ToProblemDetails();

        context.ExceptionHandled = true;
    }
}