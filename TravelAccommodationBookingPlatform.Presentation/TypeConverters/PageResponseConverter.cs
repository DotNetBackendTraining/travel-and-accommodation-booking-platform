using AutoMapper;
using TravelAccommodationBookingPlatform.Application.Shared.Responses;

namespace TravelAccommodationBookingPlatform.Presentation.TypeConverters;

public class PageResponseConverter<TSource, TDestination>
    : ITypeConverter<PageResponse<TSource>, PageResponse<TDestination>>
{
    public PageResponse<TDestination> Convert(
        PageResponse<TSource> source,
        PageResponse<TDestination> destination,
        ResolutionContext context)
    {
        return new PageResponse<TDestination>
        {
            Items = source.Items.Select(item => context.Mapper.Map<TDestination>(item)).ToList(),
            TotalCount = source.TotalCount
        };
    }
}