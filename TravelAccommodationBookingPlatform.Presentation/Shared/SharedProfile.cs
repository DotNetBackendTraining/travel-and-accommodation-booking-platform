using AutoMapper;
using Microsoft.AspNetCore.Http;
using TravelAccommodationBookingPlatform.Application.Interfaces.Files;
using TravelAccommodationBookingPlatform.Application.Shared.Pagination;
using TravelAccommodationBookingPlatform.Presentation.TypeConverters;

namespace TravelAccommodationBookingPlatform.Presentation.Shared;

public class SharedProfile : Profile
{
    public SharedProfile()
    {
        CreateMap<IFormFile, IFile>()
            .ConvertUsing<FormFileToIFileConverter>();

        CreateMap(typeof(PageResponse<>), typeof(PageResponse<>))
            .ConvertUsing(typeof(PageResponseConverter<,>));
    }
}