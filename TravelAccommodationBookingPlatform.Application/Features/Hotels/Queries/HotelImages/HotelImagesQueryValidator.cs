using FluentValidation;
using TravelAccommodationBookingPlatform.Application.Validators;

namespace TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelImages;

public class HotelImagesQueryValidator : AbstractValidator<HotelImagesQuery>
{
    public HotelImagesQueryValidator()
    {
        RuleFor(q => q.PaginationParameters)
            .SetValidator(new PaginationParametersValidator());
    }
}