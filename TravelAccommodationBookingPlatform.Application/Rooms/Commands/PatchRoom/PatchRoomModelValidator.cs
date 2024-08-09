using FluentValidation;
using TravelAccommodationBookingPlatform.Application.Validators;
using TravelAccommodationBookingPlatform.Application.Validators.Extensions;

namespace TravelAccommodationBookingPlatform.Application.Rooms.Commands.PatchRoom;

public class PatchRoomModelValidator : AbstractValidator<PatchRoomModel>
{
    public PatchRoomModelValidator()
    {
        RuleFor(x => x.HotelId)
            .NotNull().WithMessage("HotelId field cannot be removed");

        RuleFor(x => x.RoomNumber!.Value)
            .NotNull().WithMessage("RoomNumber field cannot be removed")
            .ValidRoomNumber();

        RuleFor(x => x.RoomType)
            .NotNull().WithMessage("RoomType field cannot be removed");

        RuleFor(x => x.Description!)
            .NotNull().WithMessage("Description field cannot be removed")
            .ValidRoomDescription();

        RuleFor(x => x.Price!)
            .NotNull().WithMessage("Price field cannot be removed")
            .SetValidator(new PriceValidator());

        RuleFor(x => x.MaxNumberOfGuests!)
            .NotNull().WithMessage("MaxNumberOfGuests field cannot be removed")
            .SetValidator(new NumberOfGuestsValidator());
    }
}