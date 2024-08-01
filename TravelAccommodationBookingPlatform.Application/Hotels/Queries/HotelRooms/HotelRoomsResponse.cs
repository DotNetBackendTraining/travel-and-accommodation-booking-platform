using TravelAccommodationBookingPlatform.Application.Shared.Pagination;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelRooms;

public class HotelRoomsResponse
{
    public Guid Id { get; set; }
    public required PageResponse<RoomResponse> Results { get; set; }

    public class RoomResponse
    {
        public Guid Id { get; set; }
        public int RoomNumber { get; set; }
        public RoomType RoomType { get; set; }
        public string Description { get; set; } = string.Empty;
        public Price Price { get; set; } = default!;
        public NumberOfGuests MaxNumberOfGuests { get; set; } = default!;
    }
}