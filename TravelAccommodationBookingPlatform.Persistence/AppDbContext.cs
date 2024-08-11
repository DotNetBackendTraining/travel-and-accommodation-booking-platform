using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Persistence.Configurations.Associations;

namespace TravelAccommodationBookingPlatform.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<HotelImageAssociation> HotelImageAssociations { get; set; }
    public DbSet<RoomImageAssociation> RoomImageAssociations { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override int SaveChanges()
    {
        SetTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void SetTimestamps()
    {
        var entries = ChangeTracker.Entries().Where(e =>
            e is { Entity: BaseEntity, State: EntityState.Added or EntityState.Modified });
        foreach (var entry in entries)
        {
            var now = DateTime.UtcNow;
            ((BaseEntity)entry.Entity).UpdatedAt = now;

            if (entry.State == EntityState.Added)
            {
                ((BaseEntity)entry.Entity).CreatedAt = now;
            }
        }
    }
}