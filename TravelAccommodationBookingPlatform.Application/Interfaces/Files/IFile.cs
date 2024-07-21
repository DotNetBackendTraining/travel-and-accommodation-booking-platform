namespace TravelAccommodationBookingPlatform.Application.Interfaces.Files;

public interface IFile
{
    string FileName { get; }
    Stream OpenReadStream();
}