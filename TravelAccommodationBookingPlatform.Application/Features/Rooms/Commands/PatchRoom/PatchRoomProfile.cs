using AutoMapper;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Features.Rooms.Commands.PatchRoom;

public class PatchRoomProfile : Profile
{
    public PatchRoomProfile()
    {
        CreateMap<PatchRoomModel, Room>().ReverseMap();
    }
}