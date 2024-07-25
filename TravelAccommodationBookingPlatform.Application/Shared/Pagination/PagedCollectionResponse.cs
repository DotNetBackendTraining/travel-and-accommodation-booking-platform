namespace TravelAccommodationBookingPlatform.Application.Shared.Pagination;

public class PagedCollectionResponse<TItem>
{
    public int TotalCount { get; set; }
    public ICollection<TItem> Items { get; set; } = default!;
}