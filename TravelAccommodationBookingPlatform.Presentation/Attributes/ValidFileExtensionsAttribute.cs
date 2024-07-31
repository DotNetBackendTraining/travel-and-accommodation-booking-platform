using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TravelAccommodationBookingPlatform.Presentation.Attributes;

public class ValidFileExtensionsAttribute : ValidationAttribute
{
    private readonly string[] _allowedExtensions;

    public ValidFileExtensionsAttribute(string[] allowedExtensions)
    {
        _allowedExtensions = allowedExtensions;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        switch (value)
        {
            case IFormFile file when !IsValidFileExtension(file.FileName):
                return new ValidationResult(GetErrorMessage());
            case IEnumerable<IFormFile> files:
            {
                if (files.Any(f => !IsValidFileExtension(f.FileName)))
                {
                    return new ValidationResult(GetErrorMessage());
                }

                break;
            }
        }

        return ValidationResult.Success;
    }

    private bool IsValidFileExtension(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLowerInvariant().TrimStart('.');
        return _allowedExtensions.Contains(extension);
    }

    private string GetErrorMessage()
    {
        return $"Invalid file type. Allowed types are {string.Join(", ", _allowedExtensions)}.";
    }
}