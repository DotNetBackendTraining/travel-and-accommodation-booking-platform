namespace TravelAccommodationBookingPlatform.App.DependencyInjection;

public static class PresentationServicesExtension
{
    public static void AddPresentationServices(this IServiceCollection services)
    {
        services.AddProblemDetails();
        services.AddControllers()
            .AddApplicationPart(Presentation.AssemblyReference.Assembly);
    }
}