namespace TravelAccommodationBookingPlatform.Application.Shared.Responses;

public sealed class PageResponse<TItem>
{
    public int TotalCount { get; set; }
    public ICollection<TItem> Items { get; set; } = [];
}