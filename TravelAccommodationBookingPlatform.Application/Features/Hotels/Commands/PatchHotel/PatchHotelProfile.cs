using AutoMapper;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Features.Hotels.Commands.PatchHotel;

public class PatchHotelProfile : Profile
{
    public PatchHotelProfile()
    {
        CreateMap<PatchHotelModel, Hotel>().ReverseMap();
    }
}