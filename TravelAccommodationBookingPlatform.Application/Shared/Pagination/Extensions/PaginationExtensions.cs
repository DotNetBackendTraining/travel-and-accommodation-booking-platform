namespace TravelAccommodationBookingPlatform.Application.Shared.Pagination.Extensions;

public static class PaginationExtensions
{
    public static IEnumerable<T> TakePage<T>(this IEnumerable<T> source, PaginationParameters parameters)
    {
        return source
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize);
    }
}