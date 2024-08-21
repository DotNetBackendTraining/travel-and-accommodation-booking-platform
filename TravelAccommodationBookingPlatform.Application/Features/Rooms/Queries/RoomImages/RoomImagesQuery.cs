using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Shared;

namespace TravelAccommodationBookingPlatform.Application.Features.Rooms.Queries.RoomImages;

public class RoomImagesQuery : IQuery<RoomImagesResponse>
{
    public Guid Id { get; set; }
    public PaginationParameters PaginationParameters { get; set; } = default!;
}