namespace TravelAccommodationBookingPlatform.Application.Features.Rooms.Queries.RoomDetails.Admin;

public class AdminRoomDetailsResponse
{
    public RoomDetailsResponse RoomDetails { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}