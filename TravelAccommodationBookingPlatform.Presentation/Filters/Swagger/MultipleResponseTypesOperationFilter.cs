using System.Reflection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using TravelAccommodationBookingPlatform.Presentation.Attributes;

namespace TravelAccommodationBookingPlatform.Presentation.Filters.Swagger;

public class MultipleResponseTypesOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var roleBasedResponseAttr = context.MethodInfo.GetCustomAttribute<MultipleResponseTypesAttribute>();
        if (roleBasedResponseAttr is null)
        {
            return;
        }

        var schemas = roleBasedResponseAttr.ResponseTypes
            .Select(t => context.SchemaGenerator.GenerateSchema(t, context.SchemaRepository))
            .ToList();

        operation.Responses[roleBasedResponseAttr.StatusCode.ToString()] = new OpenApiResponse
        {
            Description = "Successful response",
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["application/json"] = new()
                {
                    Schema = new OpenApiSchema
                    {
                        OneOf = schemas
                    }
                }
            }
        };
    }
}