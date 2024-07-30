using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Persistence.Configurations.PropertyBuilderExtensions;

namespace TravelAccommodationBookingPlatform.Persistence.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.Property(b => b.UserId)
            .IsRequired();

        builder.HasOne(b => b.User)
            .WithMany(u => u.Bookings)
            .HasForeignKey(b => b.UserId)
            .IsRequired();

        builder.ComplexProperty(b => b.Checking)
            .ApplyCheckingConfiguration();

        builder.HasMany(b => b.Rooms)
            .WithMany(r => r.Bookings);

        builder.ComplexProperty(b => b.NumberOfGuests)
            .ApplyNumberOfGuestsConfiguration();

        builder.ComplexProperty(b => b.SpecialRequest)
            .ApplySpecialRequestConfiguration();

        builder.HasOne(b => b.Payment)
            .WithOne()
            .HasForeignKey<Booking>(b => b.PaymentId)
            .IsRequired(false);
    }
}