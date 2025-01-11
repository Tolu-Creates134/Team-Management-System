using System;
using MediatR;
using TeamManagementSystem.Application.Common.Authentication;
using TeamManagementSystem.Application.DTOs;
using TeamManagementSystem.Application.Interfaces;
using TeamManagementSystem.Application.Users.Commands;
using TeamManagementSystem.Domain.Models;

namespace TeamManagementSystem.Application.Users.Services;

public class RegisterUserCommandHander : IRequestHandler<RegisterUserCommand, RegistrationResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticate _authenticate;

    public RegisterUserCommandHander(IUserRepository userRepository, IAuthenticate authenticate) {
        _userRepository = userRepository;
        _authenticate = authenticate;
    }

    public async Task<RegistrationResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        //var getUser = await FindUserByEmail(registerUserDTO.Email!);
        var getUser = await _userRepository.FindUserByEmailAsync(request.Email!);

        if (getUser != null) {
            return new RegistrationResponse(false, "User exists");
        }

        var newUser = new UserEntity(
            Guid.NewGuid(),
            request.FirstName,
            request.LastName,
            request.UserName,
            request.Role,
            null,
            DateTime.UtcNow,
            null,
            _authenticate.hashPassword(request.Password!),
            request.Email
        );

        await _userRepository.AddUserAsync(newUser);

        // _appDbContext.Users!.Add(new UserEntity(
        //     Guid.NewGuid(),
        //     registerUserDTO.FirstName,
        //     registerUserDTO.LastName,
        //     registerUserDTO.UserName,
        //     registerUserDTO.Role,
        //     null,
        //     DateTime.UtcNow,
        //     null,
        //     BCrypt.Net.BCrypt.HashPassword(registerUserDTO.Password),
        //     registerUserDTO.Email
        // ));

        // await _appDbContext.SaveChangesAsync();

        return new RegistrationResponse(true, "Registration Completed.", newUser);
    }
}
