using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelSearch.Dtos;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Features.Hotels.Queries.HotelSearch;

public class HotelSearchProfile : Profile
{
    public HotelSearchProfile()
    {
        CreateMap<Hotel, HotelSearchResult.HotelSummary>()
            .ForMember(hs => hs.CityName, opt => opt.MapFrom(h => h.City.Name));
    }
}