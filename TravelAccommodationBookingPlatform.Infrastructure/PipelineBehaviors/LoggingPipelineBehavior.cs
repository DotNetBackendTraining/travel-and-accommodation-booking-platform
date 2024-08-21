using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Infrastructure.PipelineBehaviors;

public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Starting request {@RequestName}, {@DateTimeUtc}",
            typeof(TRequest).Name,
            DateTime.UtcNow);

        try
        {
            var result = await next();

            if (result.IsFailure)
            {
                _logger.LogError(
                    "Request failure {@RequestName}, {@Error}, {@DateTimeUtc}",
                    typeof(TRequest).Name,
                    result.Error,
                    DateTime.UtcNow);
            }

            return result;
        }
        catch (ValidationException ex)
        {
            _logger.LogError(
                "Request validation failure {@RequestName}, {@ValidationErrors}, {@DateTimeUtc}",
                typeof(TRequest).Name,
                ex.Errors,
                DateTime.UtcNow);

            throw;
        }
        finally
        {
            _logger.LogInformation(
                "Completed request {@RequestName}, {@DateTimeUtc}",
                typeof(TRequest).Name,
                DateTime.UtcNow);
        }
    }
}