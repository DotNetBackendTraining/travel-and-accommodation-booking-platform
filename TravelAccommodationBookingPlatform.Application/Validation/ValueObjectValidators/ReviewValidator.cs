using FluentValidation;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Validation.ValueObjectValidators;

public class ReviewValidator : AbstractValidator<Review>
{
    public ReviewValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Review text is required.")
            .MaximumLength(DomainRules.Reviews.TextMaxLength)
            .WithMessage($"Review text cannot exceed {DomainRules.Reviews.TextMaxLength} characters.");
    }
}