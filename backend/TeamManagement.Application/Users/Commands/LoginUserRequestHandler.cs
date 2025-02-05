using MediatR;
using TeamManagementSystem.Application.Common.Authentication;
using TeamManagementSystem.Application.DTOs;
using TeamManagementSystem.Application.Interfaces;
using System.ComponentModel.DataAnnotations;
using TeamManagementSystem.Domain.Models;


namespace TeamManagementSystem.Application.Users.Commands;

public class LoginUserRequest : IRequest<LoginResponse>
{
    [Required, EmailAddress]
    public string Email { get; set;} = string.Empty;
    
    [Required]
    public string Password { get; set;} = string.Empty;
}

public class LoginUserRequestHandler : IRequestHandler<LoginUserRequest, LoginResponse>
{
    private readonly IUserRepository _userRepository;

    private readonly IAuthenticate _authenticate;

    public LoginUserRequestHandler(IUserRepository userRepository, IAuthenticate authenticate) {
        _userRepository = userRepository;
        _authenticate = authenticate;
    }

    public async Task<LoginResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        // Get current user entity using their email
        var user = await _userRepository.FindUserByEmailAsync(request.Email!);

        // Check if user exits if not we throw response back to UI 
        if (user == null) {
            return new LoginResponse ( false, "User does not exist, please try again with correct details.");
        }

        bool verifyPassword = _authenticate.CheckPassword(request.Password, user.PasswordHash!);

        string accessToken = _authenticate.GenerateToken(user);

        var refreshToken = new RefreshTokenEntity
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = _authenticate.GenerateRefreshtoken(),
            ExpiresOnUTC = DateTime.UtcNow.AddDays(7)
        };

        await _authenticate.AddRefreshToken(refreshToken);

        if (verifyPassword) {
            return new LoginResponse ( true, "Login Successfully", accessToken, refreshToken.Token);
        }
        else {
            return new LoginResponse ( false, "Invalid credentials");
        }
    }
}
