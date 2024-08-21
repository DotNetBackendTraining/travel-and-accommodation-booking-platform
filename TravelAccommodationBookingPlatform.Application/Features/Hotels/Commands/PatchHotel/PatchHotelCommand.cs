using TravelAccommodationBookingPlatform.Application.Interfaces.IO;
using TravelAccommodationBookingPlatform.Application.Interfaces.Messaging;

namespace TravelAccommodationBookingPlatform.Application.Features.Hotels.Commands.PatchHotel;

public class PatchHotelCommand : ICommand
{
    public Guid Id { get; set; }
    public IPatchDocument<PatchHotelModel> PatchDocument { get; set; } = default!;
}