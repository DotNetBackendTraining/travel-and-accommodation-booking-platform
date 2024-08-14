namespace TravelAccommodationBookingPlatform.Application.Interfaces.IO;

public interface IPatchDocument<in T> where T : class
{
    /// <summary>
    /// Applies patch changes to the model.
    /// </summary>
    void ApplyTo(T model);
}