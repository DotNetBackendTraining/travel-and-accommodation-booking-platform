using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TravelAccommodationBookingPlatform.Application.Interfaces.IO;
using TravelAccommodationBookingPlatform.Infrastructure.Settings;

namespace TravelAccommodationBookingPlatform.Infrastructure.IO;

public class SmtpEmailService : IEmailService
{
    private readonly SmtpSettings _smtpSettings;
    private readonly ILogger<SmtpEmailService> _logger;

    public SmtpEmailService(
        IOptions<SmtpSettings> smtpSettings,
        ILogger<SmtpEmailService> logger)
    {
        _smtpSettings = smtpSettings.Value ??
                        throw new ArgumentException(nameof(SmtpSettings) + " not found");
        _logger = logger;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
        {
            Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_smtpSettings.From),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };
        mailMessage.To.Add(email);

        try
        {
            await client.SendMailAsync(mailMessage);
            _logger.LogInformation("Email sent to {Email} successfully.", email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending email to {Email}.", email);
            throw;
        }
    }
}