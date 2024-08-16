using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TravelAccommodationBookingPlatform.Application.Interfaces.Auth;
using TravelAccommodationBookingPlatform.Application.Interfaces.IO;
using TravelAccommodationBookingPlatform.Domain.Shared;
using TravelAccommodationBookingPlatform.Infrastructure.Auth;
using TravelAccommodationBookingPlatform.Infrastructure.IO;
using TravelAccommodationBookingPlatform.Infrastructure.Settings;
using TravelAccommodationBookingPlatform.Presentation.Constants;
using TravelAccommodationBookingPlatform.Presentation.Shared.ResultExtensions;

namespace TravelAccommodationBookingPlatform.App.DependencyInjection;

public static class InfrastructureServicesExtension
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
        services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IPasswordHashService, PasswordHashService>();
        services.AddScoped<IImageStorageService, CloudinaryImageStorageService>();
        services.AddScoped<IEmailService, SmtpEmailService>();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var jwtSettings = serviceProvider.GetRequiredService<IOptions<JwtSettings>>().Value;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };

                options.Events = new JwtBearerEvents
                {
                    // Custom authorization failure to return ProblemDetails response
                    OnAuthenticationFailed = context =>
                    {
                        context.NoResult();

                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = PresentationRules.ContentTypes.ProblemJson;

                        var error = new Error(
                            ErrorType.NotAuthorized,
                            "Authorization Failed",
                            context.Exception.Message);

                        var problemDetails = Result
                            .Failure(error)
                            .ToProblemDetails()
                            .Value as ProblemDetails;

                        return context.Response.WriteAsJsonAsync(problemDetails);
                    }
                };
            });
    }
}