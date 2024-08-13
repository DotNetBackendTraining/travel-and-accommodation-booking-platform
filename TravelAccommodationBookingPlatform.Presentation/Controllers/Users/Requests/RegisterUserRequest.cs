namespace TravelAccommodationBookingPlatform.Presentation.Controllers.Users.Requests;

public class RegisterUserRequest
{
    /// <summary>
    /// The username for the new user.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// The password for the new user.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// The email address for the new user.
    /// </summary>
    public string Email { get; set; } = string.Empty;
}