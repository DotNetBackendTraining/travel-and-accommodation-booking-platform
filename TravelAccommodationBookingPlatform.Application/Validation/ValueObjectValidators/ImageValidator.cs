using FluentValidation;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Validation.ValueObjectValidators;

public class ImageValidator : AbstractValidator<Image>
{
    public ImageValidator()
    {
        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("Image URL is required.")
            .MaximumLength(DomainRules.Images.UrlMaxLength)
            .WithMessage($"Image URL cannot exceed {DomainRules.Images.UrlMaxLength} characters.")
            .Matches(@"^(http|https):\/\/[^ ""]+$").WithMessage("Invalid URL format.");
    }
}