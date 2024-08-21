using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Features.Rooms.Commands.CreateRoom;
using TravelAccommodationBookingPlatform.Application.Features.Rooms.Queries.RoomDetails;
using TravelAccommodationBookingPlatform.Application.Features.Rooms.Queries.RoomDetails.Admin;
using TravelAccommodationBookingPlatform.Presentation.Features.Rooms.Requests;
using TravelAccommodationBookingPlatform.Presentation.Features.Rooms.ViewModels;

namespace TravelAccommodationBookingPlatform.Presentation.Features.Rooms;

public class RoomProfile : Profile
{
    public RoomProfile()
    {
        // CreateRoom
        CreateMap<CreateRoomRequest, CreateRoomCommand>();

        // RoomDetails
        CreateMap<RoomDetailsResponse, RoomDetailsViewModel>();
        CreateMap<RoomDetailsResponse, AdminRoomDetailsViewModel>();
        CreateMap<AdminRoomDetailsResponse, AdminRoomDetailsViewModel>()
            .IncludeMembers(src => src.RoomDetails);
    }
}