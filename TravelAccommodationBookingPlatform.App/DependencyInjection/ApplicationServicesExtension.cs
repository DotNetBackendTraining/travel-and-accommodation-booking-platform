namespace TravelAccommodationBookingPlatform.App.DependencyInjection;

public static class ApplicationServicesExtension
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly));
    }
}