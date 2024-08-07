using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Shared;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch;

public class HotelSearchQuery : IQuery<HotelSearchResponse>
{
    public PaginationParameters PaginationParameters { get; set; } = default!;
    public HotelSearchFilters Filters { get; set; } = new();
    public HotelSearchOptions Options { get; set; } = new();

    public class HotelSearchFilters
    {
        public string SearchTerm { get; set; } = string.Empty;
        public GeneralFilters General { get; set; } = new();
        public AdvancedFilters Advanced { get; set; } = new();
    }

    public class HotelSearchOptions
    {
        public SortingOption SortingOption { get; set; } = SortingOption.Name;
        public bool IncludeAvailableSearchFilters { get; set; }
        public bool IncludePriceDealIfAvailable { get; set; }
    }

    public class GeneralFilters
    {
        public Checking? Checking { get; set; }
        public NumberOfGuests? NumberOfGuests { get; set; }
        public int? Rooms { get; set; }
    }

    public class AdvancedFilters
    {
        public Price? MinPrice { get; set; }
        public Price? MaxPrice { get; set; }
        public List<StarRate>? AllowedStarRatings { get; set; }
        public List<Amenity>? RequiredAmenities { get; set; }
        public List<RoomType>? RequiredRoomTypes { get; set; }
    }

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