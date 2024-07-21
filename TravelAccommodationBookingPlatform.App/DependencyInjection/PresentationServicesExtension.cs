using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using TravelAccommodationBookingPlatform.App.Filters;

namespace TravelAccommodationBookingPlatform.App.DependencyInjection;

public static class PresentationServicesExtension
{
    public static void AddPresentationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Presentation.AssemblyReference.Assembly);

        services.AddProblemDetails();
        services.AddControllers(options => options.Filters.Add(new ValidationExceptionFilter()))
            .AddApplicationPart(Presentation.AssemblyReference.Assembly);
    }

    public static void AddSwaggerDocumentation(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Add fluent validation rules documentation
        services.AddFluentValidationRulesToSwagger(options =>
        {
            options.SetNotNullableIfMinLengthGreaterThenZero = true;
            options.UseAllOfForMultipleRules = true;
        });

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Authenticated Web App API", Version = "v1" });

            // Add XML output from Presentation assembly
            var assembly = Presentation.AssemblyReference.Assembly;
            var xmlFile = $"{assembly.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            // Add JWT authentication support in swagger
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });
    }
}