using FluentValidation;
using TravelAccommodationBookingPlatform.Application.Validators;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.FeaturedHotels;

public class FeaturedHotelsQueryValidator : AbstractValidator<FeaturedHotelsQuery>
{
    public FeaturedHotelsQueryValidator()
    {
        RuleFor(q => q.PaginationParameters)
            .SetValidator(new PaginationParametersValidator());
    }
}