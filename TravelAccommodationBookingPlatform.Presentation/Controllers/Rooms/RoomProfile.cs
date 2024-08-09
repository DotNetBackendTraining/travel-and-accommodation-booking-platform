using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Rooms.Commands.CreateRoom;
using TravelAccommodationBookingPlatform.Presentation.Controllers.Rooms.Requests;

namespace TravelAccommodationBookingPlatform.Presentation.Controllers.Rooms;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        CreateMap<CreateRoomRequest, CreateRoomCommand>();
    }
}