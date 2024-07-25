using TravelAccommodationBookingPlatform.Application.Shared.Pagination;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelImages;

public class HotelImagesResponse : PagedCollectionResponse<Image>
{
    public Guid Id { get; set; }
}