using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Shared;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch;

public class HotelSearchQuery : IQuery<HotelSearchResponse>
{
    public HotelSearchFilters Filters { get; set; } = new();
    public PaginationParameters PaginationParameters { get; set; } = default!;
    public bool IncludeAvailableSearchFilters { get; set; }
    public bool IncludePriceDealIfAvailable { get; set; }

    public class HotelSearchFilters
    {
        public string SearchTerm { get; set; } = string.Empty;
        public GeneralFilters General { get; set; } = new();
        public AdvancedFilters Advanced { get; set; } = new();
    }

    public class GeneralFilters
    {
        public Checking Checking { get; set; } = new()
        {
            CheckInDate = DateTime.Today,
            CheckOutDate = DateTime.Today.AddDays(1)
        };

        public NumberOfGuests NumberOfGuests { get; set; } = new()
        {
            Adults = 2,
            Children = 0
        };

        public int Rooms { get; set; } = 1;
    }

    public class AdvancedFilters
    {
        public Price? MinPrice { get; set; }
        public Price? MaxPrice { get; set; }
        public List<StarRate>? AllowedStarRatings { get; set; }
        public List<Amenity>? RequiredAmenities { get; set; }
        public List<RoomType>? RequiredRoomTypes { get; set; }
    }
}