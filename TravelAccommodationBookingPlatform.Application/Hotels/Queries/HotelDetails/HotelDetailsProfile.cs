using AutoMapper;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelDetails;

public class HotelDetailsProfile : Profile
{
    public HotelDetailsProfile()
    {
        CreateMap<Hotel, HotelDetailsResponse>()
            .ForMember(hd => hd.CityName, opt => opt.MapFrom(h => h.City.Name));
    }
}