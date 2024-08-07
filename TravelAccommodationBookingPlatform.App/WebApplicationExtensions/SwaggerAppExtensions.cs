namespace TravelAccommodationBookingPlatform.App.WebApplicationExtensions;

public static class SwaggerAppExtensions
{
    public static void UseSwaggerWithConfigurations(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var description in app.DescribeApiVersions())
            {
                options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });
    }
}