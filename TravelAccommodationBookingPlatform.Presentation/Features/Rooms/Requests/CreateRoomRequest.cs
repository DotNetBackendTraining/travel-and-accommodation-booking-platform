using Microsoft.AspNetCore.Http;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;
using TravelAccommodationBookingPlatform.Presentation.Attributes;

namespace TravelAccommodationBookingPlatform.Presentation.Features.Rooms.Requests;

public class CreateRoomRequest
{
    /// <summary>
    /// The unique identifier of the hotel where the room is being created.
    /// </summary>
    public Guid HotelId { get; set; }

    /// <summary>
    /// The room number for the new room.
    /// </summary>
    public int RoomNumber { get; set; }

    /// <summary>
    /// The type of the room, which can be Luxury, Budget, or Boutique.
    /// </summary>
    public RoomType RoomType { get; set; }

    /// <summary>
    /// A description of the room, detailing its features.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The price of the room per night.
    /// </summary>
    public Price Price { get; set; } = default!;

    /// <summary>
    /// The maximum number of guests that the room can accommodate, including adults and children.
    /// </summary>
    public NumberOfGuests MaxNumberOfGuests { get; set; } = default!;

    /// <summary>
    /// A collection of images representing the room.
    /// </summary>
    [ValidImageExtensions]
    public ICollection<IFormFile> Images { get; set; } = [];
}