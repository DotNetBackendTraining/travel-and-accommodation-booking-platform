using Microsoft.EntityFrameworkCore;
using TravelAccommodationBookingPlatform.Persistence;

namespace TravelAccommodationBookingPlatform.App.DependencyInjection;

public static class PersistenceServicesExtension
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("AppDbContextConnection") ??
                                 throw new ArgumentException("AppDbContextConnection not found")));
    }
}