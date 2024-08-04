using TravelAccommodationBookingPlatform.Application.Shared.Pagination;
using TravelAccommodationBookingPlatform.Application.Shared.Responses;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.FeaturedHotels;

public class FeaturedHotelsResponse
{
    public required PageResponse<FeaturedDealResult> Results { get; set; }

    public class FeaturedDealResult
    {
        public FeaturedHotelSummary Hotel { get; set; } = default!;
        public PriceDealResponse MinimumPriceDeal { get; set; } = default!;
        public PriceDealResponse MaximumPriceDeal { get; set; } = default!;
    }

    public class FeaturedHotelSummary
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public StarRate StarRate { get; set; }
        public string Description { get; set; } = string.Empty;
        public Image ThumbnailImage { get; set; } = default!;
        public Guid CityId { get; set; }
        public string CityName { get; set; } = string.Empty;
    }
}