using AutoMapper;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Features.Cities.Commands.CreateCity;

public class CreateCityProfile : Profile
{
    public CreateCityProfile()
    {
        CreateMap<CreateCityCommand, City>()
            .ForMember(dest => dest.ThumbnailImage, opt => opt.Ignore());
    }
}