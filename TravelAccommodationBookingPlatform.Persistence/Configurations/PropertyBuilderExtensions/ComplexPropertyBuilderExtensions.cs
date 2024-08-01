using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Persistence.Configurations.PropertyBuilderExtensions;

public static class ComplexPropertyBuilderExtensions
{
    public static ComplexPropertyBuilder<T> ApplyCoordinatesConfiguration<T>(this ComplexPropertyBuilder<T> builder)
        where T : Coordinates
    {
        builder.Property(c => c.Latitude)
            .IsRequired()
            .HasPrecision(DomainRules.Locations.PrecisionDigits, DomainRules.Locations.ScaleDigits)
            .HasAnnotation("Min", DomainRules.Locations.LatitudeMin)
            .HasAnnotation("Max", DomainRules.Locations.LatitudeMax);

        builder.Property(c => c.Longitude)
            .IsRequired()
            .HasPrecision(DomainRules.Locations.PrecisionDigits, DomainRules.Locations.ScaleDigits)
            .HasAnnotation("Min", DomainRules.Locations.LongitudeMin)
            .HasAnnotation("Max", DomainRules.Locations.LongitudeMax);

        return builder;
    }

    public static ComplexPropertyBuilder<T> ApplyPriceConfiguration<T>(this ComplexPropertyBuilder<T> builder)
        where T : Price
    {
        builder.Property(p => p.Value)
            .IsRequired()
            .HasAnnotation("Min", DomainRules.Prices.PriceMin);

        return builder;
    }

    public static ComplexPropertyBuilder<T> ApplyNumberOfGuestsConfiguration<T>(this ComplexPropertyBuilder<T> builder)
        where T : NumberOfGuests
    {
        builder.Property(g => g.Adults)
            .IsRequired()
            .HasAnnotation("Min", DomainRules.NumberOfGuests.AdultsMin)
            .HasAnnotation("Max", DomainRules.NumberOfGuests.AdultsMax);

        builder.Property(g => g.Children)
            .IsRequired()
            .HasAnnotation("Min", DomainRules.NumberOfGuests.ChildrenMin)
            .HasAnnotation("Max", DomainRules.NumberOfGuests.ChildrenMax);

        return builder;
    }

    public static ComplexPropertyBuilder<T> ApplyCountryConfiguration<T>(this ComplexPropertyBuilder<T> builder)
        where T : Country
    {
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(DomainRules.Countries.NameMaxLength);

        return builder;
    }

    public static ComplexPropertyBuilder<T> ApplyPostOfficeConfiguration<T>(this ComplexPropertyBuilder<T> builder)
        where T : PostOffice
    {
        builder.Property(p => p.Address)
            .IsRequired()
            .HasMaxLength(DomainRules.PostOffices.AddressMaxLength);

        builder.Property(p => p.Description)
            .HasMaxLength(DomainRules.PostOffices.DescriptionMaxLength);

        return builder;
    }

    public static ComplexPropertyBuilder<T> ApplySpecialRequestConfiguration<T>(this ComplexPropertyBuilder<T> builder)
        where T : SpecialRequest
    {
        builder.Property(sr => sr.Request)
            .HasMaxLength(DomainRules.SpecialRequests.RequestMaxLength);

        return builder;
    }

    public static ComplexPropertyBuilder<T> ApplyCheckingConfiguration<T>(this ComplexPropertyBuilder<T> builder)
        where T : Checking
    {
        builder.Property(c => c.CheckInDate)
            .IsRequired();

        builder.Property(c => c.CheckOutDate)
            .IsRequired();

        return builder;
    }

    public static ComplexPropertyBuilder<T> ApplyPersonalInformationConfiguration<T>(
        this ComplexPropertyBuilder<T> builder)
        where T : PersonalInformation
    {
        builder.Property(pi => pi.FullName)
            .IsRequired()
            .HasMaxLength(DomainRules.PersonalInformation.FullNameMaxLength);

        builder.Property(pi => pi.PhoneNumber)
            .IsRequired()
            .HasMaxLength(DomainRules.PersonalInformation.PhoneNumberMaxLength);

        return builder;
    }

    public static ComplexPropertyBuilder<T> ApplyDiscountRateConfiguration<T>(this ComplexPropertyBuilder<T> builder)
        where T : DiscountRate
    {
        builder.Property(dr => dr.Percentage)
            .IsRequired()
            .HasAnnotation("Min", DomainRules.DiscountRates.PercentageMin)
            .HasAnnotation("Max", DomainRules.DiscountRates.PercentageMax);

        return builder;
    }
}