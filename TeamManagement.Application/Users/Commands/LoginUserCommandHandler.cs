using System;
using MediatR;
using TeamManagementSystem.Application.Common.Authentication;
using TeamManagementSystem.Application.DTOs;
using TeamManagementSystem.Application.Interfaces;

namespace TeamManagementSystem.Application.Users.Commands;

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

        //bool checkPassword = BCrypt.Net.BCrypt.Verify(loginDTO.Password, getUser.PasswordHash);

        bool verifyPassword = _authenticate.checkPassword(request.Password, getUser.PasswordHash!);

        if (verifyPassword) {
            //return new LoginResponse ( true, "Login Successfully", GenerateJWTToken(getUser));
            return new LoginResponse ( true, "Login Successfully", _authenticate.generateToken(getUser));
        }
        else {
            return new LoginResponse ( false, "Invalid credentials");
        }
    }
}
