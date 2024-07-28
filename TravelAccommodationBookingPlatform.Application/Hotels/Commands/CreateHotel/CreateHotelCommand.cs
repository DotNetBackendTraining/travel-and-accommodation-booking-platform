using TravelAccommodationBookingPlatform.Application.Interfaces.Files;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;
using TravelAccommodationBookingPlatform.Domain.Enums;
using TravelAccommodationBookingPlatform.Domain.ValueObjects;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Commands.CreateHotel;

public class CreateHotelCommand : ICommand<CreateHotelResponse>
{
    public string Name { get; set; } = string.Empty;
    public int StarRate { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Owner { get; set; } = string.Empty;
    public IFile ThumbnailImage { get; set; } = default!;
    public IEnumerable<IFile> Images { get; set; } = [];
    public Guid CityId { get; set; }
    public Coordinates Coordinates { get; set; } = default!;
    public IList<Amenity> Amenities { get; set; } = [];
}