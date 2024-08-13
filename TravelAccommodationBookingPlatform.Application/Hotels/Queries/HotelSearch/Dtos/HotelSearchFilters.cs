using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.DTOs;

public class HotelSearchFilters
{
    /// <summary>
    /// The search term to use when searching for hotels (e.g., hotel name or city name).
    /// </summary>
    public string SearchTerm { get; set; } = string.Empty;

    /// <summary>
    /// General filters like checking dates, number of guests, and number of rooms.
    /// </summary>
    public GeneralFilters General { get; set; } = new();

    /// <summary>
    /// Advanced filters such as price range, star ratings, amenities, and room types.
    /// </summary>
    public AdvancedFilters Advanced { get; set; } = new();

    public class GeneralFilters
    {
        /// <summary>
        /// The check-in and check-out dates for the hotel search.
        /// Any hotel with no rooms available in those dates will be disqualified from the search
        /// </summary>
        public Checking? Checking { get; set; }

        /// <summary>
        /// The number of guests for the hotel booking.
        /// Any hotel without enough rooms to fit the number of guests will be disqualified from the search
        /// </summary>
        public NumberOfGuests? NumberOfGuests { get; set; }

        /// <summary>
        /// The number of rooms required for the booking.
        /// Any hotel will lower number of rooms will be disqualified from the search
        /// </summary>
        public int? Rooms { get; set; }
    }

    public class AdvancedFilters
    {
        /// <summary>
        /// The minimum price of the hotel stay.
        /// Any hotel with rooms priced less than the minimum price will be disqualified from the search
        /// </summary>
        public Price? MinPrice { get; set; }

        /// <summary>
        /// The maximum price of the hotel stay.
        /// Any hotel with rooms priced more than the maximum price will be disqualified from the search
        /// </summary>
        public Price? MaxPrice { get; set; }

        /// <summary>
        /// A list of allowed star ratings for the hotels in the search results.
        /// Any hotel with a different start rate will be disqualified from the search
        /// </summary>
        public List<StarRate>? AllowedStarRatings { get; set; }

        /// <summary>
        /// A list of required amenities for the hotels in the search results.
        /// Any hotel with at least one missing amenity will be disqualified from the search
        /// </summary>
        public List<Amenity>? RequiredAmenities { get; set; }

        /// <summary>
        /// A list of required room types for the hotels in the search results.
        /// Any hotel with at least one missing room type will be disqualified from the search
        /// </summary>
        public List<RoomType>? RequiredRoomTypes { get; set; }
    }
}