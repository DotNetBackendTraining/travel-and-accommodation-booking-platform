using TravelAccommodationBookingPlatform.App.DependencyInjection;
using TravelAccommodationBookingPlatform.App.WebApplicationExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program;