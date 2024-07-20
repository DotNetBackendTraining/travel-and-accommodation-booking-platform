using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;

public interface IUserRepository
{
    public Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken = default);

    public Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);

    public void AddUser(User user);
}