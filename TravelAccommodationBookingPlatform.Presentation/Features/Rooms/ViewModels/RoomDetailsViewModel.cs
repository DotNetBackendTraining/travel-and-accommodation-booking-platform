using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Presentation.Features.Rooms.ViewModels;

public class RoomDetailsViewModel
{
    public Guid HotelId { get; set; }
    public int RoomNumber { get; set; }
    public RoomType RoomType { get; set; }
    public string Description { get; set; } = string.Empty;
    public Price Price { get; set; } = default!;
    public NumberOfGuests MaxNumberOfGuests { get; set; } = default!;
}