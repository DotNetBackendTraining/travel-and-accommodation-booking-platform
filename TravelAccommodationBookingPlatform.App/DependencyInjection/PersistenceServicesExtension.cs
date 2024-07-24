using Microsoft.EntityFrameworkCore;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Persistence;
using TravelAccommodationBookingPlatform.Persistence.Repositories;

namespace TravelAccommodationBookingPlatform.App.DependencyInjection;

public static class PersistenceServicesExtension
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.UseSqlServer(configuration.GetConnectionString("AppDbContextConnection") ??
                                 throw new ArgumentException("AppDbContextConnection not found"));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}