using FluentValidation;
using TravelAccommodationBookingPlatform.Application.Shared.Validators;

namespace TravelAccommodationBookingPlatform.Application.Rooms.Queries.RoomImages;

public class RoomImagesQueryValidator : AbstractValidator<RoomImagesQuery>
{
    public RoomImagesQueryValidator()
    {
        RuleFor(q => q.PaginationParameters)
            .SetValidator(new PaginationParametersValidator());
    }
}