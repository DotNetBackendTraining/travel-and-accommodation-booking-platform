using FluentValidation;
using TravelAccommodationBookingPlatform.Application.Validators;

namespace TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingSearch;

public class BookingSearchQueryValidator : AbstractValidator<BookingSearchQuery>
{
    public BookingSearchQueryValidator()
    {
        RuleFor(q => q.PaginationParameters)
            .SetValidator(new PaginationParametersValidator());

        RuleFor(x => x.Filters.CheckingInStartDate)
            .LessThanOrEqualTo(x => x.Filters.CheckingInEndDate)
            .When(x => x.Filters is { CheckingInStartDate: not null, CheckingInEndDate: not null })
            .WithMessage("CheckingInStartDate must be less than or equal to CheckingInEndDate.");

        RuleFor(x => x.Filters.CheckingOutStartDate)
            .LessThanOrEqualTo(x => x.Filters.CheckingOutEndDate)
            .When(x => x.Filters is { CheckingOutStartDate: not null, CheckingOutEndDate: not null })
            .WithMessage("CheckingOutStartDate must be less than or equal to CheckingOutEndDate.");

        RuleFor(x => x.Filters.Timespan)
            .IsInEnum().WithMessage("Timespan must be a valid enum value.");
    }
}