using TravelAccommodationBookingPlatform.Infrastructure.Settings;

namespace TravelAccommodationBookingPlatform.App.DependencyInjection;

public static class InfrastructureServicesExtension
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
    }
}