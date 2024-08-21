using TravelAccommodationBookingPlatform.Application.Interfaces.IO;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;

namespace TravelAccommodationBookingPlatform.Application.Features.Rooms.Commands.PatchRoom;

public class PatchRoomCommand : ICommand
{
    public Guid Id { get; set; }
    public IPatchDocument<PatchRoomModel> PatchDocument { get; set; } = default!;
}