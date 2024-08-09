using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Rooms.Commands.PatchRoom;

public class PatchRoomModel
{
    public Guid? HotelId { get; set; }
    public int? RoomNumber { get; set; }
    public RoomType? RoomType { get; set; }
    public string? Description { get; set; }
    public Price? Price { get; set; }
    public NumberOfGuests? MaxNumberOfGuests { get; set; }
}