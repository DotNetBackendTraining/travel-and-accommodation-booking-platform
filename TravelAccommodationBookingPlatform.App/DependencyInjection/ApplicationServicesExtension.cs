using FluentValidation;
using MediatR;
using TravelAccommodationBookingPlatform.Application.Behaviors;

namespace TravelAccommodationBookingPlatform.App.DependencyInjection;

public static class ApplicationServicesExtension
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly));

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

        services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly);
    }
}