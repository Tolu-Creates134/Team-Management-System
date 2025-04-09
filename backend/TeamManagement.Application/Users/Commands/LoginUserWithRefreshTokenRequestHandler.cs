using MediatR;
using TeamManagementSystem.Application.Common.Interfaces;
using TeamManagementSystem.Domain.Models;

namespace TeamManagementSystem.Application.Users.Commands;

public sealed record RefreshTokenRequest (string RefreshToken) : IRequest<RefreshTokenResponse>;

public sealed record RefreshTokenResponse (string AccessToken, string RefreshToken);

public sealed  class LoginUserWithRefreshTokenRequestHandler : IRequestHandler<RefreshTokenRequest, RefreshTokenResponse>
{
    private readonly IAuthenticate _authenticate;
    public LoginUserWithRefreshTokenRequestHandler (IAuthenticate authenticate)
    {
        _authenticate = authenticate;
    }

    public async Task<RefreshTokenResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        RefreshTokenEntity? refreshToken = await _authenticate.GetRefreshToken(request.RefreshToken);

        if (refreshToken == null || refreshToken.ExpiresOnUTC < DateTime.UtcNow)
        {
            throw new ApplicationException("The refresh token is expired");
        }

        // Creating new access token for user
        string accessToken = _authenticate.GenerateToken(refreshToken.User!);

        //Create new refresh token for user also
        refreshToken.Token = _authenticate.GenerateRefreshtoken();

        refreshToken.ExpiresOnUTC = DateTime.UtcNow.AddDays(7);

        await _authenticate.UpdateRefreshToken(refreshToken);

        return new RefreshTokenResponse (accessToken, refreshToken.Token);
    }
}
