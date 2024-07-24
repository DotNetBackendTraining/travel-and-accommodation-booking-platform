using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Persistence.Configurations.PropertyBuilderExtensions;

namespace TravelAccommodationBookingPlatform.Persistence.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasOne(r => r.Hotel)
            .WithMany(h => h.Rooms)
            .HasForeignKey(r => r.HotelId);

        builder.Property(r => r.RoomNumber)
            .IsRequired()
            .HasAnnotation("Min", DomainRules.Rooms.RoomNumberMin)
            .HasAnnotation("Max", DomainRules.Rooms.RoomNumberMax);

        builder.Property(r => r.Description)
            .IsRequired()
            .HasMaxLength(DomainRules.Rooms.DescriptionMaxLength);

        builder.ComplexProperty(r => r.Price)
            .ApplyPriceConfiguration();

        builder.ComplexProperty(r => r.MaxNumberOfGuests)
            .ApplyNumberOfGuestsConfiguration();

        builder.OwnsMany(r => r.Images, img =>
        {
            img.WithOwner().HasForeignKey("HotelId");
            img.Property<int>("Id");
            img.HasKey("Id");
            img.ApplyImageConfiguration();
        }).Navigation(e => e.Images).AutoInclude(false);

        builder.HasMany(r => r.Bookings);
    }
}