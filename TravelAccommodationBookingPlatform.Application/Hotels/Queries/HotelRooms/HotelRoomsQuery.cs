using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Shared.Pagination;
using TravelAccommodationBookingPlatform.Domain.Enums;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelRooms;

public class HotelRoomsQuery : IQuery<HotelRoomsResponse>
{
    public Guid Id { get; set; }
    public PaginationParameters PaginationParameters { get; set; } = default!;
    public RoomType? RoomType { get; set; }
}