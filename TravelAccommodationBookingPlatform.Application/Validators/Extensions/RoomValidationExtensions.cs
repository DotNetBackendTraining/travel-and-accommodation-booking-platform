using FluentValidation;
using TravelAccommodationBookingPlatform.Domain.Constants;

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
}