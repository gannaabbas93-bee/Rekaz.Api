namespace Rekaz.Api.Core.Interfaces;

using Rekaz.Api.Core.Entities;

public interface IJwtService
{
    string GenerateToken(User user);
}
