namespace TravelAccommodationBookingPlatform.Presentation.Shared;

/// <summary>
/// Custom ProblemDetails object.
/// Defines error response type that controller endpoints produce.
/// </summary>
public sealed class ErrorResponse
{
    public string Type { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public List<Error> Errors { get; set; } = [];

    public sealed class Error
    {
        public string Code { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}