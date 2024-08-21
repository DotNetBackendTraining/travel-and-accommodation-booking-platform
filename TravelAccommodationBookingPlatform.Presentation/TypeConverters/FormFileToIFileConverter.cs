using AutoMapper;
using Microsoft.AspNetCore.Http;
using TravelAccommodationBookingPlatform.Application.Interfaces.IO;
using TravelAccommodationBookingPlatform.Presentation.Shared.Wrappers;

namespace TravelAccommodationBookingPlatform.Presentation.TypeConverters;

public class FormFileToIFileConverter : ITypeConverter<IFormFile, IFile>
{
    public IFile Convert(IFormFile source, IFile destination, ResolutionContext context)
    {
        return new FormFileWrapper(source);
    }
}