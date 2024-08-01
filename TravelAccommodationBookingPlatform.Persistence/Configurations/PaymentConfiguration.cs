using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Persistence.Configurations.PropertyBuilderExtensions;

namespace TravelAccommodationBookingPlatform.Persistence.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasOne(p => p.Booking)
            .WithOne(b => b.Payment)
            .HasForeignKey<Payment>(p => p.BookingId)
            .IsRequired();

        builder.ComplexProperty(p => p.PersonalInformation)
            .ApplyPersonalInformationConfiguration()
            .IsRequired();

        builder.HasOne(p => p.AppliedDiscount)
            .WithMany()
            .HasForeignKey(p => p.AppliedDiscountId);

        builder.Property(p => p.ConfirmationNumber)
            .IsRequired()
            .HasMaxLength(DomainRules.Payments.ConfirmationNumberMaxLength);

        builder.Property(p => p.Status)
            .IsRequired();
    }
}