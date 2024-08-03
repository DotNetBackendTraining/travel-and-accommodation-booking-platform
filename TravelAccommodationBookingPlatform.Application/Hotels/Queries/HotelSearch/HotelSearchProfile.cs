using AutoMapper;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch;

public class HotelSearchProfile : Profile
{
    public HotelSearchProfile()
    {
        CreateMap<Hotel, HotelSearchResponse.HotelSummary>()
            .ForMember(hs => hs.CityName, opt => opt.MapFrom(h => h.City.Name));
    }
}