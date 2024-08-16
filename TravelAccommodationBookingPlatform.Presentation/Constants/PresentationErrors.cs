using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Presentation.Constants;

public static class PresentationErrors
{
    public static Error AuthenticationFailed(string message) => new(
        ErrorType.NotAuthorized,
        "Authorization Failed",
        message);
}