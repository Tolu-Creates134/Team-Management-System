using System;
using TeamManagementSystem.Application.DTOs;

namespace TeamManagementSystem.Application.Interfaces;

public interface IUserRepository
{
    Task<RegistrationResponse> RegisterUserAsync(RegisterUserDTO registerUserDTO);

    Task<LoginResponse> LoginUserAsync(LoginDTO loginDTO);

}
