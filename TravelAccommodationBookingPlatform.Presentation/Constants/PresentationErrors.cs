using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Presentation.Constants;

public static class PresentationErrors
{
    public static Error AuthenticationFailed(string message) => new(
        ErrorType.NotAuthorized,
        "Authorization Failed",
        message);

    public static readonly Error TooManyRequests = new(
        ErrorType.TooManyRequests,
        "Rate limit exceeded",
        "You have sent too many requests in a given time frame. Please try again later.");
}