using TravelAccommodationBookingPlatform.Application.Shared.Responses;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers.Hotels.ViewModels;

public class HotelSearchViewModel
{
    /// <summary>
    /// The parameters to control the pagination of the search results.
    /// </summary>
    public required PageResponse<HotelSearchResult> SearchResults { get; set; }

    /// <summary>
    /// The filters that are available for refining the search results.
    /// This includes price ranges, star ratings, amenities, and room types.
    /// </summary>
    public AvailableFiltersResult? AvailableFilters { get; set; }

    /// <summary>
    /// A single hotel search result.
    /// </summary>
    public class HotelSearchResult
    {
        /// <summary>
        /// The unique identifier of the hotel.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the hotel.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The star rating of the hotel.
        /// </summary>
        public StarRate StarRate { get; set; }

        /// <summary>
        /// A brief description of the hotel.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// The thumbnail image representing the hotel.
        /// </summary>
        public Image ThumbnailImage { get; set; } = default!;

        /// <summary>
        /// The unique identifier of the city where the hotel is located.
        /// </summary>
        public Guid CityId { get; set; }

        /// <summary>
        /// The name of the city where the hotel is located.
        /// </summary>
        public string CityName { get; set; } = string.Empty;

        /// <summary>
        /// The deal for the minimum price available for the hotel.
        /// </summary>
        public PriceDealResponse? MinimumPriceDeal { get; set; } = default!;

        /// <summary>
        /// The deal for the maximum price available for the hotel.
        /// </summary>
        public PriceDealResponse? MaximumPriceDeal { get; set; } = default!;
    }

    /// <summary>
    /// The filters that are available for refining the hotel search results.
    /// </summary>
    public class AvailableFiltersResult
    {
        /// <summary>
        /// The minimum price available among the search results.
        /// </summary>
        public Price MinimumPrice { get; set; } = default!;

        /// <summary>
        /// The maximum price available among the search results.
        /// </summary>
        public Price MaximumPrice { get; set; } = default!;

        /// <summary>
        /// A list of star ratings available among the search results.
        /// </summary>
        public IList<StarRate> AvailableStarRatings { get; set; } = default!;

        /// <summary>
        /// A list of amenities available among the search results.
        /// </summary>
        public IList<Amenity> AvailableAmenities { get; set; } = default!;

        /// <summary>
        /// A list of room types available among the search results.
        /// </summary>
        public IList<RoomType> AvailableRoomTypes { get; set; } = default!;
    }
}