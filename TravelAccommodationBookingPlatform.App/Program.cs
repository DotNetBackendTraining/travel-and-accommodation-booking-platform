using Autofac.Extensions.DependencyInjection;
using Serilog;
using TravelAccommodationBookingPlatform.App.DependencyInjection;
using TravelAccommodationBookingPlatform.App.WebApplicationExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddPresentationServices();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerDocumentation();
}

var app = builder.Build();

await app.Migrate();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithConfigurations();
}

app.UseHttpsRedirection();
app.UseSerilogRequestLogging();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();

public partial class Program;