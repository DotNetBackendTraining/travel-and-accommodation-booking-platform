using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Shared.Pagination;

namespace TravelAccommodationBookingPlatform.Application.Rooms.Queries.RoomImages;

public class RoomImagesQuery : IQuery<RoomImagesResponse>
{
    public Guid Id { get; set; }
    public PaginationParameters PaginationParameters { get; set; } = default!;
}