using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelImages;

public sealed class HotelImagesSpecification : Specification<Hotel, HotelImagesResponse>
{
    public HotelImagesSpecification(HotelImagesQuery query)
    {
        Query.Select(h => new HotelImagesResponse
        {
            Id = h.Id,
            Items = h.Images
                .AsQueryable()
                .OrderBy(i => i.Url)
                .Skip((query.PaginationParameters.PageNumber - 1) * query.PaginationParameters.PageSize)
                .Take(query.PaginationParameters.PageSize)
                .ToList(),
            TotalCount = h.Images.Count
        }).Where(h => h.Id == query.Id);
    }
}