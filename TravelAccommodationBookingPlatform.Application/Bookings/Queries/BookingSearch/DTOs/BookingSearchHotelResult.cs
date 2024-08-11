using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Bookings.Queries.BookingSearch.DTOs;

public class BookingSearchHotelResult
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public StarRate StarRate { get; set; }
    public Image ThumbnailImage { get; set; } = default!;
    public Price? MinimumPrice { get; set; }
    public Price? MaximumPrice { get; set; }
}