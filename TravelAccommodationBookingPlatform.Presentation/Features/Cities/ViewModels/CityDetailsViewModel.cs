using TravelAccommodationBookingPlatform.Domain.Entities;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Presentation.Features.Cities.ViewModels;

public class CityDetailsViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Country Country { get; set; } = default!;
    public PostOffice PostOffice { get; set; } = default!;
    public Image ThumbnailImage { get; set; } = default!;
}