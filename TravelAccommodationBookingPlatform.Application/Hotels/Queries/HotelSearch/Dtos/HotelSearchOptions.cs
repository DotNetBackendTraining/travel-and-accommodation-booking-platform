namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.DTOs;

public class HotelSearchOptions
{
    public SortingOption Sorting { get; set; } = SortingOption.Name;
    public bool IncludeAvailableSearchFilters { get; set; }
    public bool IncludePriceDealIfAvailable { get; set; }

    public enum SortingOption
    {
        Name = 0,

        /// <summary>
        /// Sorts by feature score, filters any hotel that is not featured
        /// </summary>
        Featured = 1,

        StarsThenName = 2
    }
}