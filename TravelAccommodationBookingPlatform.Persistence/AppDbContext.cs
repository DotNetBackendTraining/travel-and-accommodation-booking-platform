using Microsoft.EntityFrameworkCore;

namespace TravelAccommodationBookingPlatform.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}