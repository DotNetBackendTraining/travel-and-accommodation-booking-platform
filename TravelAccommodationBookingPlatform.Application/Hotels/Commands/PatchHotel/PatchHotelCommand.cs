using TravelAccommodationBookingPlatform.Application.Interfaces.Files;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;

namespace TravelAccommodationBookingPlatform.Application.Hotels.Commands.PatchHotel;

public class PatchHotelCommand : ICommand
{
    public Guid Id { get; set; }
    public IPatchDocument<PatchHotelModel> PatchDocument { get; set; } = default!;
}