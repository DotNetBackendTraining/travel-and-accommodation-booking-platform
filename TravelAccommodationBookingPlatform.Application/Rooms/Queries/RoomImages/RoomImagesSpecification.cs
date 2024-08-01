using Ardalis.Specification;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Rooms.Queries.RoomImages;

public sealed class RoomImagesSpecification : Specification<Room, RoomImagesResponse>
{
    public RoomImagesSpecification(RoomImagesQuery query)
    {
        Query.Select(r => new RoomImagesResponse
        {
            Id = r.Id,
            Results = new()
            {
                Items = r.Images
                    .AsQueryable()
                    .OrderBy(i => i.Url)
                    .Skip((query.PaginationParameters.PageNumber - 1) * query.PaginationParameters.PageSize)
                    .Take(query.PaginationParameters.PageSize)
                    .ToList(),
                TotalCount = r.Images.Count
            }
        }).Where(r => r.Id == query.Id);
    }
}