using MediatR;
using TeamManagementSystem.Application.Common.Authentication;
using TeamManagementSystem.Application.DTOs;
using TeamManagementSystem.Application.Interfaces;
using System.ComponentModel.DataAnnotations;
using TeamManagementSystem.Domain.Models;
using TeamManagementSystem.Application.Common.Behaviours;


namespace TeamManagementSystem.Application.Users.Commands;

public class LoginUserRequest : IRequest<LoginResponse>, ISkipValidation
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
        if (user == null) {
            throw new UnauthorizedAccessException("User does not exist, please try again with correct details."); // Send 401
        }

        bool verifyPassword = _authenticate.CheckPassword(request.Password, user.PasswordHash!);
        if (!verifyPassword){
            throw new UnauthorizedAccessException("Invalid credentials"); // Send 401
        }
        
        string accessToken = _authenticate.GenerateToken(user);

        var refreshToken = new RefreshTokenEntity
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = _authenticate.GenerateRefreshtoken(),
            ExpiresOnUTC = DateTime.UtcNow.AddDays(7)
        };

        await _authenticate.AddRefreshToken(refreshToken);

        return new LoginResponse ( true, "Login Successfully", accessToken, refreshToken.Token);
    }
}
