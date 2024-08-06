using FluentValidation;
using TravelAccommodationBookingPlatform.Application.Validators;
using TravelAccommodationBookingPlatform.Application.Validators.Extensions;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Commands.CreateHotel;

public class CreateHotelCommandValidator : AbstractValidator<CreateHotelCommand>
{
    public CreateHotelCommandValidator()
    {
        RuleFor(x => x.Name)
            .ValidHotelName();

        RuleFor(x => x.Description)
            .ValidHotelDescription();

        RuleFor(x => x.Owner)
            .ValidOwner();

        RuleFor(x => x.ThumbnailImage)
            .NotNull().WithMessage("Thumbnail image is required.");

        RuleFor(x => x.CityId)
            .NotEmpty().WithMessage("City ID is required.");

        RuleFor(x => x.Coordinates)
            .SetValidator(new CoordinatesValidator());
    }
}