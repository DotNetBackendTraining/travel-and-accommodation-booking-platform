using AutoMapper;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Features.Rooms.Commands.CreateRoom;

public class CreateRoomProfile : Profile
{
    public CreateRoomProfile()
    {
        CreateMap<CreateRoomCommand, Room>()
            .ForMember(dest => dest.Images, opt => opt.Ignore());
    }
}