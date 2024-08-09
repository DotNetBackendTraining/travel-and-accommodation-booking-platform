using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Commands.DeleteHotel.Specifications;

public sealed class HotelByIdIncludingImagesSpecification : Specification<Hotel>
{
    public HotelByIdIncludingImagesSpecification(Guid id)
    {
        Query.Where(h => h.Id == id)
            .Include(h => h.ThumbnailImage)
            .Include(h => h.Images);
    }
}