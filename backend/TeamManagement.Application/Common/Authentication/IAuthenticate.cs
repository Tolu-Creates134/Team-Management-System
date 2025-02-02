using TeamManagementSystem.Domain.Models;

namespace TeamManagementSystem.Application.Common.Authentication;

public interface IAuthenticate
{
    string generateToken(UserEntity user);

    bool checkPassword(string loginPasword, string DBPassword);

    string hashPassword(string password);

    string generateRefreshtoken();

    Task addRefreshToken (RefreshTokenEntity refreshToken);
}
