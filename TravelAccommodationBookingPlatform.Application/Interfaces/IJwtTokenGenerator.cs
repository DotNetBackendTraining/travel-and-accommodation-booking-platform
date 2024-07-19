using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}