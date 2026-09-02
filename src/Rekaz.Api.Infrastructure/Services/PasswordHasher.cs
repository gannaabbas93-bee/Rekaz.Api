namespace Rekaz.Api.Infrastructure.Services;

using BCrypt.Net;
using Rekaz.Api.Core.Interfaces;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        return BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Verify(password, passwordHash);
    }
}
