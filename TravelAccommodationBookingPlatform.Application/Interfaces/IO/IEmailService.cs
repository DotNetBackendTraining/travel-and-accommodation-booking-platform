namespace TravelAccommodationBookingPlatform.Application.Interfaces.IO;

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string message);
}