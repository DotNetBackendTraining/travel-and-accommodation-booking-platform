using TravelAccommodationBookingPlatform.Domain.Enums;

namespace TravelAccommodationBookingPlatform.Application.Features.Bookings.Queries.BookingDetails.DTOs;

public class BookingDetailsHotelResult
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public StarRate StarRate { get; set; }
    public string Description { get; set; } = string.Empty;
}