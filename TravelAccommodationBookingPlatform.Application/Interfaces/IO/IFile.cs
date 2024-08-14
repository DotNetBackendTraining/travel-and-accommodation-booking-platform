namespace TravelAccommodationBookingPlatform.Application.Interfaces.IO;

public interface IFile
{
    string FileName { get; }
    Stream OpenReadStream();
}