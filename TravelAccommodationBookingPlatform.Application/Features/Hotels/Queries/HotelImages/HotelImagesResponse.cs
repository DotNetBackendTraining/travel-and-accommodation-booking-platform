using TravelAccommodationBookingPlatform.Application.Shared.Responses;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelImages;

public class HotelImagesResponse
{
    public Guid Id { get; set; }
    public required PageResponse<Image> Results { get; set; }
}