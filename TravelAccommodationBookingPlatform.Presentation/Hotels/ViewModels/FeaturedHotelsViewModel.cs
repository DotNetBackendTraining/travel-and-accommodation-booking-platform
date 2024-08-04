using TravelAccommodationBookingPlatform.Application.Shared.Pagination;
using TravelAccommodationBookingPlatform.Application.Shared.Responses;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Presentation.Hotels.ViewModels;

public class FeaturedHotelsViewModel
{
    public required PageResponse<FeaturedHotelResult> Results { get; set; }

    public class FeaturedHotelResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public StarRate StarRate { get; set; }
        public string Description { get; set; } = string.Empty;
        public Image ThumbnailImage { get; set; } = default!;
        public Guid CityId { get; set; }
        public string CityName { get; set; } = string.Empty;
        public PriceDeal PriceDeal { get; set; } = default!;
    }

    public class PriceDeal
    {
        public PriceDealResponse Minimum { get; set; } = default!;
        public PriceDealResponse Maximum { get; set; } = default!;
    }
}