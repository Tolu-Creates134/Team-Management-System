using TeamManagementSystem.Domain.Models;

namespace TeamManagementSystem.Application.Common.Interfaces;

public interface IAuthenticate
{
    string GenerateToken(UserEntity user);

    bool CheckPassword(string loginPasword, string DBPassword);

    string HashPassword(string password);

    string GenerateRefreshtoken();

    Task AddRefreshToken (RefreshTokenEntity refreshToken);

    Task UpdateRefreshToken(RefreshTokenEntity refreshToken);

    Task<RefreshTokenEntity?> GetRefreshToken(string refreshToken);
}
