using TravelAccommodationBookingPlatform.Application.Interfaces.Files;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Rooms.Commands.CreateRoom;

public class CreateRoomCommand : ICommand<CreateRoomResponse>
{
    public Guid HotelId { get; set; }
    public int RoomNumber { get; set; }
    public RoomType RoomType { get; set; }
    public string Description { get; set; } = string.Empty;
    public Price Price { get; set; } = default!;
    public NumberOfGuests MaxNumberOfGuests { get; set; } = default!;
    public ICollection<IFile> Images { get; set; } = default!;
}