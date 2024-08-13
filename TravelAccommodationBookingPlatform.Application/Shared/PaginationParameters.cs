namespace TravelAccommodationBookingPlatform.Application.Shared;

/// <summary>
/// The pagination parameters to control the pagination of the requested results.
/// </summary>
public class PaginationParameters
{
    /// <summary>
    /// The current page number to retrieve.
    /// Must be a positive integer.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// The number of items to include on each page of results.
    /// Must be a positive integer.
    /// </summary>
    public int PageSize { get; set; }
}