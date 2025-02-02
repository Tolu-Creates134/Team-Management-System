using MediatR;
using TeamManagementSystem.Application.Common.Authentication;
using TeamManagementSystem.Application.DTOs;
using TeamManagementSystem.Application.Interfaces;
using System.ComponentModel.DataAnnotations;
using TeamManagementSystem.Domain.Models;


namespace TeamManagementSystem.Application.Users.Commands;

public class LoginUserCommand : IRequest<LoginResponse>
{
    [Required, EmailAddress]
    public string Email { get; set;} = string.Empty;
    
    [Required]
    public string Password { get; set;} = string.Empty;
}

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginResponse>
{
    private readonly IUserRepository _userRepository;

    private readonly IAuthenticate _authenticate;

    public LoginUserCommandHandler(IUserRepository userRepository, IAuthenticate authenticate) {
        _userRepository = userRepository;
        _authenticate = authenticate;
    }

    public async Task<LoginResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var getUser = await _userRepository.FindUserByEmailAsync(request.Email!);

        if (getUser == null) {
            return new LoginResponse ( false, "User does not exist, please try again with correct details.");
        }

        bool verifyPassword = _authenticate.checkPassword(request.Password, getUser.PasswordHash!);

        string accessToken = _authenticate.generateToken(getUser);

        var refreshToken = new RefreshTokenEntity
        {
            Id = Guid.NewGuid(),
            UserId = getUser.Id,
            Token = _authenticate.generateRefreshtoken(),
            ExpiresOnUTC = DateTime.UtcNow.AddDays(7)
        };

        await _authenticate.addRefreshToken(refreshToken);

        if (verifyPassword) {
            return new LoginResponse ( true, "Login Successfully", accessToken, refreshToken.Token);
        }
        else {
            return new LoginResponse ( false, "Invalid credentials");
        }
    }
}
