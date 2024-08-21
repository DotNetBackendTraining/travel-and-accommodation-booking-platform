using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;

namespace TravelAccommodationBookingPlatform.Application.Features.Rooms.Queries.RoomDetails;

public class RoomDetailsQuery : IQuery<RoomDetailsResponse>
{
    public Guid Id { get; set; }
}