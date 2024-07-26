using AutoMapper;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Rooms.Queries.RoomDetails;

public class RoomDetailsProfile : Profile
{
    public RoomDetailsProfile()
    {
        CreateMap<Room, RoomDetailsResponse>();
    }
}