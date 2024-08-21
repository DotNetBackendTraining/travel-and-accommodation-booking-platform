using FluentValidation;
using TravelAccommodationBookingPlatform.Application.Validators;

namespace TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelReviews;

public class HotelReviewsQueryValidator : AbstractValidator<HotelReviewsQuery>
{
    public HotelReviewsQueryValidator()
    {
        RuleFor(q => q.PaginationParameters)
            .SetValidator(new PaginationParametersValidator());
    }
}