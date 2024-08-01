using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Persistence.Configurations.PropertyBuilderExtensions;

namespace TravelAccommodationBookingPlatform.Persistence.Configurations;

public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.HasOne(d => d.Hotel)
            .WithMany(h => h.Discounts)
            .HasForeignKey(d => d.HotelId)
            .IsRequired();

        builder.ComplexProperty(d => d.Rate)
            .ApplyDiscountRateConfiguration()
            .IsRequired();
    }
}