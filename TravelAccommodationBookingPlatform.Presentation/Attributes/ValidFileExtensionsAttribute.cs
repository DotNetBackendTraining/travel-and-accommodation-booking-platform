using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TravelAccommodationBookingPlatform.Presentation.Attributes;

public class ValidFileExtensionsAttribute : ValidationAttribute
{
    private static readonly char[] Separator = [','];
    private readonly string[] _allowedExtensions;

    /// <param name="allowedExtensions">Separated by <see cref="Separator"/></param>
    public ValidFileExtensionsAttribute(string allowedExtensions)
    {
        _allowedExtensions = allowedExtensions.Split(Separator, StringSplitOptions.RemoveEmptyEntries)
            .Select(x => x.Trim().ToLowerInvariant())
            .ToArray();
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