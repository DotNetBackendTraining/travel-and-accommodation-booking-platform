using TravelAccommodationBookingPlatform.Application.Shared.Responses;
using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingSearch;

public class BookingSearchResponse
{
    public PageResponse<BookingSearchResult> Results { get; set; } = default!;

    public class BookingSearchResult
    {
        public Checking Checking { get; set; } = default!;
        public BookingHotelSummary Hotel { get; set; } = default!;
    }

    public class BookingHotelSummary
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public StarRate StarRate { get; set; }
        public Image ThumbnailImage { get; set; } = default!;
        public Price? MinimumPrice { get; set; }
        public Price? MaximumPrice { get; set; }
    }
}