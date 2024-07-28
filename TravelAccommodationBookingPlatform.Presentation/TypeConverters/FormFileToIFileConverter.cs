using AutoMapper;
using Microsoft.AspNetCore.Http;
using TravelAccommodationBookingPlatform.Application.Interfaces.Files;
using TravelAccommodationBookingPlatform.Presentation.Shared;

namespace TravelAccommodationBookingPlatform.Presentation.TypeConverters;

public class FormFileToIFileConverter : ITypeConverter<IFormFile, IFile>
{
    public IFile Convert(IFormFile source, IFile destination, ResolutionContext context)
    {
        return new FormFileWrapper(source);
    }
}