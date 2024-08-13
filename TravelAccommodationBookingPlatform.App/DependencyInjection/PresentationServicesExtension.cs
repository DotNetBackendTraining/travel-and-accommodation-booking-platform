using System.Reflection;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using TravelAccommodationBookingPlatform.Presentation.Filters;

namespace TravelAccommodationBookingPlatform.App.DependencyInjection;

public static class PresentationServicesExtension
{
    public static void AddPresentationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Presentation.AssemblyReference.Assembly);

        services.AddProblemDetails();
        services.AddControllers(options =>
            {
                options.Filters.Add(new ValidationExceptionFilter());
                options.Filters.Add(new JsonPatchExceptionFilter());
            })
            .AddApplicationPart(Presentation.AssemblyReference.Assembly)
            .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()));

        services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
    }

    public static void AddSwaggerDocumentation(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Add Newtonsoft.Json Support
        services.AddSwaggerGenNewtonsoftSupport();

        // Add fluent validation rules documentation
        services.AddFluentValidationRulesToSwagger(options =>
        {
            options.SetNotNullableIfMinLengthGreaterThenZero = true;
            options.UseAllOfForMultipleRules = true;
        });

        services.AddSwaggerGen(options =>
        {
            var apiVersionDescriptionProvider = services
                .BuildServiceProvider()
                .GetRequiredService<IApiVersionDescriptionProvider>();

            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, new OpenApiInfo
                {
                    Title = $"Travel and Accommodation Booking Platform API {description.ApiVersion}",
                    Version = description.ApiVersion.ToString()
                });
            }

            // Add XML output from documented assemblies
            var documentedAssemblies = new List<Assembly>
            {
                Presentation.AssemblyReference.Assembly,
                Application.AssemblyReference.Assembly
            };
            foreach (var assembly in documentedAssemblies)
            {
                var xmlFile = $"{assembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            }

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