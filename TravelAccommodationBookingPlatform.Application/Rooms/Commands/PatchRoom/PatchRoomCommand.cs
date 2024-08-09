using TravelAccommodationBookingPlatform.Application.Interfaces.Files;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;

namespace TravelAccommodationBookingPlatform.Application.Rooms.Commands.PatchRoom;

public class PatchRoomCommand : ICommand
{
    public Guid Id { get; set; }
    public IPatchDocument<PatchRoomModel> PatchDocument { get; set; } = default!;
}