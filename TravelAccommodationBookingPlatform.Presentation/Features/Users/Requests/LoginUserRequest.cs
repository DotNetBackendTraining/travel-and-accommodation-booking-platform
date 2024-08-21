namespace TravelAccommodationBookingPlatform.Presentation.Features.Users.Requests;

public class LoginUserRequest
{
    /// <summary>
    /// The username of the user attempting to log in.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// The password of the user attempting to log in.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}