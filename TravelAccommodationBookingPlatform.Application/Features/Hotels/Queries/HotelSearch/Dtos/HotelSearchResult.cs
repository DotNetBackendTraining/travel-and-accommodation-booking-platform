using TravelAccommodationBookingPlatform.Application.Shared.Responses;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Enums;

namespace TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelSearch.Dtos;

public class HotelSearchResult
{
    public HotelSummary Summary { get; set; } = default!;
    public HotelPriceDeal? PriceDeal { get; set; }

    public class HotelSummary
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public StarRate StarRate { get; set; }
        public string Description { get; set; } = string.Empty;
        public Image ThumbnailImage { get; set; } = default!;
        public Guid CityId { get; set; }
        public string CityName { get; set; } = string.Empty;
    }

    public class HotelPriceDeal
    {
        public PriceCalculationResponse MinimumPriceDeal { get; set; } = default!;
        public PriceCalculationResponse MaximumPriceDeal { get; set; } = default!;
    }
}