using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;

public interface IUserRepository
{
    public Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken = default);
}