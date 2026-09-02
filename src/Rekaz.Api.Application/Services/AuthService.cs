namespace Rekaz.Api.Application.Services;

using Rekaz.Api.Application.Interfaces;
using Rekaz.Api.Core.DTOs.Auth;
using Rekaz.Api.Core.Entities;
using Rekaz.Api.Core.Enums;
using Rekaz.Api.Core.Interfaces;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;

    public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto dto, CancellationToken cancellationToken = default)
    {
        var cleanEmail = dto.Email.ToLowerInvariant().Trim();
        if (await _userRepository.EmailExistsAsync(cleanEmail, cancellationToken))
        {
            throw new InvalidOperationException("Email is already registered.");
        }

        var user = new User
        {
            FullName = dto.FullName.Trim(),
            Email = cleanEmail,
            PasswordHash = _passwordHasher.HashPassword(dto.Password),
            Role = UserRole.User,
            CreatedAt = DateTime.UtcNow
        };

        var createdUser = await _userRepository.AddAsync(user, cancellationToken);
        var token = _jwtService.GenerateToken(createdUser);

        return new AuthResponseDto
        {
            Id = createdUser.Id,
            FullName = createdUser.FullName,
            Email = createdUser.Email,
            Role = createdUser.Role.ToString(),
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddHours(2)
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto dto, CancellationToken cancellationToken = default)
    {
        var cleanEmail = dto.Email.ToLowerInvariant().Trim();
        var user = await _userRepository.GetByEmailAsync(cleanEmail, cancellationToken);
        if (user == null || !_passwordHasher.VerifyPassword(dto.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        var token = _jwtService.GenerateToken(user);

        return new AuthResponseDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role.ToString(),
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddHours(2)
        };
    }
}
