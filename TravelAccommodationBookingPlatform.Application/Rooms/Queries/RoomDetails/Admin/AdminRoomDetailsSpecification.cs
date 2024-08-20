using Ardalis.Specification;
using AutoMapper;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Rooms.Queries.RoomDetails.Admin;

public sealed class AdminRoomDetailsSpecification : Specification<Room, AdminRoomDetailsResponse>
{
    public AdminRoomDetailsSpecification(Guid id, IMapper mapper)
    {
        Query.Select(r => new AdminRoomDetailsResponse
            {
                RoomDetails = mapper.Map<RoomDetailsResponse>(r),
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt
            })
            .Where(r => r.Id == id)
            .EnableCache(nameof(AdminRoomDetailsResponse), id);
    }
}