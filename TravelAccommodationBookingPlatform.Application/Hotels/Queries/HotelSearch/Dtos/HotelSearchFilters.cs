using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.DTOs;

public class HotelSearchFilters
{
    public string SearchTerm { get; set; } = string.Empty;
    public GeneralFilters General { get; set; } = new();
    public AdvancedFilters Advanced { get; set; } = new();

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
}