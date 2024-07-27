using TravelAccommodationBookingPlatform.Domain.Shared;

namespace TravelAccommodationBookingPlatform.Application.Constants;

public static class ApplicationErrors
{
    public static class File
    {
        public static readonly Error UploadFailed = new(
            ErrorType.InternalServerError,
            "File.UploadFailed",
            "An error occurred while uploading the file");

        public static readonly Error DeletionFailed = new(
            ErrorType.InternalServerError,
            "File.DeletionFailed",
            "An error occurred while deleting the file");
    }
}