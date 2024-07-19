using Microsoft.EntityFrameworkCore;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
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