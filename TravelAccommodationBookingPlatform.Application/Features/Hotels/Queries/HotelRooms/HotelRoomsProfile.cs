using AutoMapper;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelRooms;

public class HotelRoomsProfile : Profile
{
    public HotelRoomsProfile()
    {
        CreateMap<Room, HotelRoomsResponse.RoomResponse>();
    }
}