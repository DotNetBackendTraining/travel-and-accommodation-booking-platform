using AutoMapper;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Cities.Commands.PatchCity;

public class PatchCityProfile : Profile
{
    public PatchCityProfile()
    {
        CreateMap<PatchCityModel, City>().ReverseMap();
    }
}