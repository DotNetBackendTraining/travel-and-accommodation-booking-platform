using Ardalis.Specification;
using AutoMapper;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelRooms;

public sealed class HotelRoomsSpecification : Specification<Hotel, HotelRoomsResponse>
{
    public HotelRoomsSpecification(HotelRoomsQuery query, IMapper mapper)
    {
        Query.Select(h => new HotelRoomsResponse
            {
                Id = h.Id,
                Items = h.Rooms
                    .AsQueryable()
                    .Where(r => query.RoomType == null || r.RoomType == query.RoomType)
                    .OrderBy(i => i.RoomNumber)
                    .Skip((query.PaginationParameters.PageNumber - 1) * query.PaginationParameters.PageSize)
                    .Take(query.PaginationParameters.PageSize)
                    .Select(r => mapper.Map<HotelRoomsResponse.RoomResponse>(r))
                    .ToList(),
                TotalCount = h.Rooms.Count
            })
            .Where(h => h.Id == query.Id);
    }
}