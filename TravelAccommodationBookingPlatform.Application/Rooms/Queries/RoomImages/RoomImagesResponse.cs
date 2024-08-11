using TravelAccommodationBookingPlatform.Application.Shared.Responses;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Rooms.Queries.RoomImages;

public class RoomImagesResponse
{
    public Guid Id { get; set; }
    public required PageResponse<Image> Results { get; set; }
}