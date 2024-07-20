using Microsoft.EntityFrameworkCore;
using TravelAccommodationBookingPlatform.Application.Interfaces.Repositories;
using TravelAccommodationBookingPlatform.Domain.Entities;

namespace TravelAccommodationBookingPlatform.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByUsername(string username, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .Where(u => u.Username == username)
            .FirstOrDefaultAsync(cancellationToken);
    }
}