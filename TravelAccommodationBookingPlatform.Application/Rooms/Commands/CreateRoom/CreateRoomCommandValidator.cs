using FluentValidation;
using TravelAccommodationBookingPlatform.Application.Validators;
using TravelAccommodationBookingPlatform.Application.Validators.Extensions;

namespace TravelAccommodationBookingPlatform.Application.Rooms.Commands.CreateRoom;

public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
{
    public CreateRoomCommandValidator()
    {
        RuleFor(x => x.RoomNumber)
            .ValidRoomNumber();

        RuleFor(x => x.Description)
            .ValidRoomDescription();

        RuleFor(x => x.Price)
            .SetValidator(new PriceValidator());

        RuleFor(x => x.MaxNumberOfGuests)
            .SetValidator(new NumberOfGuestsValidator());
    }
}