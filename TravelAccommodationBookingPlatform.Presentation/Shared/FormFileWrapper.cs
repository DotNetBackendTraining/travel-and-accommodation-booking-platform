using Microsoft.AspNetCore.Http;
using TravelAccommodationBookingPlatform.Application.Interfaces.Files;

namespace TravelAccommodationBookingPlatform.Presentation.Shared;

public class FormFileWrapper : IFile
{
    private readonly IFormFile _formFile;

    public FormFileWrapper(IFormFile formFile)
    {
        _formFile = formFile;
    }

    public string FileName => _formFile.FileName;

    public Stream OpenReadStream()
    {
        return _formFile.OpenReadStream();
    }
}