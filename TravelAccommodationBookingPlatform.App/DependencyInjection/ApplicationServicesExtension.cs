using FluentValidation;
using MediatR;
using TravelAccommodationBookingPlatform.Application.Behaviors;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Shared.Notifications;

namespace TravelAccommodationBookingPlatform.App.DependencyInjection;

public static class ApplicationServicesExtension
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly));

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly);

        services.AddAutoMapper(Application.AssemblyReference.Assembly);
    }

    /// <summary>
    /// Use this to register an ITransactionHandler,
    /// otherwise MediatR will a separate transient service to handle each notification
    /// </summary>
    internal static void AddScopedTransactionHandler(
        this IServiceCollection services,
        Func<IServiceProvider, ITransactionHandler> implementationFactory)
    {
        services.AddScoped<INotificationHandler<TransactionStartNotification>>(implementationFactory);
        services.AddScoped<INotificationHandler<TransactionSuccessNotification>>(implementationFactory);
        services.AddScoped<INotificationHandler<TransactionFailureNotification>>(implementationFactory);
        services.AddScoped<INotificationHandler<TransactionCleanupNotification>>(implementationFactory);
    }
}