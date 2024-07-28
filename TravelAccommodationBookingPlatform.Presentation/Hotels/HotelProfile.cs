using AutoMapper;
using Microsoft.AspNetCore.Http;
using TravelAccommodationBookingPlatform.Application.Hotels.Commands.CreateHotel;
using TravelAccommodationBookingPlatform.Application.Interfaces.Files;
using TravelAccommodationBookingPlatform.Presentation.Hotels.Requests;
using TravelAccommodationBookingPlatform.Presentation.TypeConverters;

namespace TravelAccommodationBookingPlatform.Presentation.Hotels;

public class HotelProfile : Profile
{
    public HotelProfile()
    {
        CreateMap<IFormFile, IFile>().ConvertUsing<FormFileToIFileConverter>();
        CreateMap<CreateHotelRequest, CreateHotelCommand>();
    }
}