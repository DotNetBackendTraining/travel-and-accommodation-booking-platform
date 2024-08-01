namespace TravelAccommodationBookingPlatform.Application.Shared.Pagination;

public sealed class PageResponse<TItem>
{
    public int TotalCount { get; set; }
    public ICollection<TItem> Items { get; set; } = [];
}