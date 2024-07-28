using AutoMapper;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Commands.CreateHotel;

public class CreateHotelProfile : Profile
{
    public CreateHotelProfile()
    {
        CreateMap<CreateHotelCommand, Hotel>()
            .ForMember(dest => dest.ThumbnailImage, opt => opt.Ignore())
            .ForMember(dest => dest.Images, opt => opt.Ignore());
    }
}