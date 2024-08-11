using Microsoft.AspNetCore.Http;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;
using TravelAccommodationBookingPlatform.Presentation.Attributes;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers.Rooms.Requests;

public class CreateRoomRequest
{
    public Guid HotelId { get; set; }
    public int RoomNumber { get; set; }
    public RoomType RoomType { get; set; }
    public string Description { get; set; } = string.Empty;
    public Price Price { get; set; } = default!;
    public NumberOfGuests MaxNumberOfGuests { get; set; } = default!;
    [ValidImageExtensions] public ICollection<IFormFile> Images { get; set; } = [];
}