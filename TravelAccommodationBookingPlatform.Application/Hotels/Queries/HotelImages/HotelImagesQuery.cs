using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Application.Shared.Pagination;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelImages;

public class HotelImagesQuery : IQuery<HotelImagesResponse>
{
    public Guid Id { get; set; }
    public PaginationParameters PaginationParameters { get; set; } = default!;
}