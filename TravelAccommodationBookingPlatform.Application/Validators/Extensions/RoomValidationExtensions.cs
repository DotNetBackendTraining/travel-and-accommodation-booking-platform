using FluentValidation;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Validators.Extensions;

public static class RoomValidationExtensions
{
    public static IRuleBuilderOptions<T, int> ValidRoomNumber<T>(this IRuleBuilder<T, int> ruleBuilder)
    {
        return ruleBuilder
            .InclusiveBetween(DomainRules.Rooms.RoomNumberMin, DomainRules.Rooms.RoomNumberMax)
            .WithMessage(
                $"Room number must be between {DomainRules.Rooms.RoomNumberMin} and {DomainRules.Rooms.RoomNumberMax}.");
    }

    public static IRuleBuilderOptions<T, string> ValidRoomDescription<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .MaximumLength(DomainRules.Rooms.DescriptionMaxLength)
            .WithMessage($"Room description cannot exceed {DomainRules.Rooms.DescriptionMaxLength} characters.");
    }

    public static IRuleBuilderOptions<T, Price> ValidPrice<T>(this IRuleBuilder<T, Price> ruleBuilder)
    {
        return ruleBuilder
            .SetValidator(new PriceValidator());
    }

    public static IRuleBuilderOptions<T, NumberOfGuests> ValidMaxNumberOfGuests<T>(
        this IRuleBuilder<T, NumberOfGuests> ruleBuilder)
    {
        return ruleBuilder
            .SetValidator(new NumberOfGuestsValidator());
    }

    public static IRuleBuilder<T, IEnumerable<Image>> MustHaveValidImages<T>(
        this IRuleBuilder<T, IEnumerable<Image>> ruleBuilder)
    {
        return ruleBuilder.ForEach(x =>
            x.SetValidator(new ImageValidator()));
    }
}