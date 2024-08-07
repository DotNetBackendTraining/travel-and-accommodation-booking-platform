using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch.DTOs;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Queries.HotelSearch;

public class HotelSearchProfile : Profile
{
    public HotelSearchProfile()
    {
        CreateMap<Hotel, HotelSearchResult.HotelSummary>()
            .ForMember(hs => hs.CityName, opt => opt.MapFrom(h => h.City.Name));
    }
}