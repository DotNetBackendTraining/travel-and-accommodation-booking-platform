using FluentValidation;
using TravelAccommodationBookingPlatform.Domain.Constants;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Validators;

public class PersonalInformationValidator : AbstractValidator<PersonalInformation>
{
    public PersonalInformationValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Full name is required.")
            .MaximumLength(DomainRules.PersonalInformation.FullNameMaxLength).WithMessage(
                $"Full name cannot exceed {DomainRules.PersonalInformation.FullNameMaxLength} characters.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .MaximumLength(DomainRules.PersonalInformation.PhoneNumberMaxLength).WithMessage(
                $"Phone number cannot exceed {DomainRules.PersonalInformation.PhoneNumberMaxLength} characters.");
    }
}