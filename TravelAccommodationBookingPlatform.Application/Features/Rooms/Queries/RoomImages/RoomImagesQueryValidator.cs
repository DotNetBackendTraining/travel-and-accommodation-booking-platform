using FluentValidation;
using TravelAccommodationBookingPlatform.Application.Validators;

namespace TravelAccommodationBookingPlatform.Application.Features.Rooms.Queries.RoomImages;

public class RoomImagesQueryValidator : AbstractValidator<RoomImagesQuery>
{
    public RoomImagesQueryValidator()
    {
        RuleFor(q => q.PaginationParameters)
            .SetValidator(new PaginationParametersValidator());
    }
}