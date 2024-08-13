namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.DTOs;

public class HotelSearchOptions
{
    /// <summary>
    /// The sorting option to apply to the search results.
    /// Default is by hotel name.
    /// </summary>
    public SortingOption Sorting { get; set; } = SortingOption.Name;

    /// <summary>
    /// Indicates whether the available search filters should be included in the response.
    /// </summary>
    public bool IncludeAvailableSearchFilters { get; set; }

    /// <summary>
    /// Indicates whether to include price deals if available for the hotels in the search results.
    /// </summary>
    public bool IncludePriceDealIfAvailable { get; set; }

    public enum SortingOption
    {
        /// <summary>
        /// Sort by hotel name.
        /// </summary>
        Name = 0,

        /// <summary>
        /// Sort by feature score, and filters any hotel that is not featured.
        /// </summary>
        Featured = 1,

        /// <summary>
        /// Sort by stars first, then by hotel name.
        /// </summary>
        StarsThenName = 2
    }
}