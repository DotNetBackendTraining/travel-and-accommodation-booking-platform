using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken = default);

    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);

    void AddUser(User user);
}