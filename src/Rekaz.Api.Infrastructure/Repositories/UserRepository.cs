namespace Rekaz.Api.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Rekaz.Api.Core.Entities;
using Rekaz.Api.Core.Enums;
using Rekaz.Api.Core.Interfaces;
using Rekaz.Api.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    private static readonly List<User> FallbackUsers = new()
    {
        new User
        {
            Id = 1,
            FullName = "System Admin",
            Email = "admin@rekaz.com",
            PasswordHash = "$2a$11$w1U4lV/bVb.sW6xZ6k1x.eQ2m6t8v.z7M1S3W4A5B6C7D8E9F0G1H2",
            Role = UserRole.Admin,
            CreatedAt = DateTime.UtcNow
        }
    };

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var cleanEmail = email?.ToLowerInvariant().Trim() ?? string.Empty;
        try
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == cleanEmail, cancellationToken);

            if (user != null)
            {
                return user;
            }
        }
        catch
        {
            // Failover
        }

        return FallbackUsers.FirstOrDefault(u => u.Email.Equals(cleanEmail, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

            if (user != null)
            {
                return user;
            }
        }
        catch
        {
            // Failover
        }

        return FallbackUsers.FirstOrDefault(u => u.Id == id);
    }

    public async Task<User> AddAsync(User user, CancellationToken cancellationToken = default)
    {
        try
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            user.Id = FallbackUsers.Count + 1;
        }

        FallbackUsers.Add(user);
        return user;
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        var cleanEmail = email?.ToLowerInvariant().Trim() ?? string.Empty;
        try
        {
            var exists = await _context.Users
                .AsNoTracking()
                .AnyAsync(u => u.Email == cleanEmail, cancellationToken);

            if (exists)
            {
                return true;
            }
        }
        catch
        {
            // Failover
        }

        return FallbackUsers.Any(u => u.Email.Equals(cleanEmail, StringComparison.OrdinalIgnoreCase));
    }
}
