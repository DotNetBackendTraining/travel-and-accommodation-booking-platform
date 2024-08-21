using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;

namespace TravelAccommodationBookingPlatform.Application.Features.Rooms.Queries.RoomDetails.Admin;

public class AdminRoomDetailsQuery : IQuery<AdminRoomDetailsResponse>
{
    public Guid Id { get; set; }
}