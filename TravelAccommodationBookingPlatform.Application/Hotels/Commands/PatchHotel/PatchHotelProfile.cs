using AutoMapper;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Commands.PatchHotel;

public class PatchHotelProfile : Profile
{
    public PatchHotelProfile()
    {
        CreateMap<PatchHotelModel, Hotel>().ReverseMap();
    }
}