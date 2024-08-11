using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;

namespace TravelAccommodationBookingPlatform.Application.Rooms.Commands.DeleteRoom;

public class DeleteRoomCommand : ICommand
{
    public Guid Id { get; set; }
}