using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TeamManagementSystem.Application.DTOs;
using TeamManagementSystem.Application.Interfaces;
using TeamManagementSystem.Domain.Models;
using TeamManagementSystem.Infrastructure.Data;


namespace TeamManagementSystem.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext) {
        _appDbContext = appDbContext;
    }

    public async Task<LoginResponse> LoginUserAsync(LoginDTO loginDTO)
    {
        var getUser = await FindUserByEmail(loginDTO.Email!);

        if (getUser == null) {
            return new LoginResponse ( false, "User does not exist, please try again with correct details.");
        }

        bool checkPassword = BCrypt.Net.BCrypt.Verify(loginDTO.Password, getUser.PasswordHash);

        if (checkPassword) {
            return new LoginResponse ( true, "Login Successfully", GenerateJWTToken(getUser));
        }
        else {
            return new LoginResponse ( false, "Invalid credentials");
        }
    }

    private async Task<UserEntity?> FindUserByEmail(string email) {
        return await _appDbContext.Users!.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<RegistrationResponse> RegisterUserAsync(RegisterUserDTO registerUserDTO)
    {
        var getUser = await FindUserByEmail(registerUserDTO.Email!);

        if (getUser != null) {
            return new RegistrationResponse(false, "User exists");
        }

        _appDbContext.Users!.Add(new UserEntity(
            Guid.NewGuid(),
            registerUserDTO.FirstName,
            registerUserDTO.LastName,
            registerUserDTO.UserName,
            registerUserDTO.Role,
            null,
            DateTime.UtcNow,
            null,
            BCrypt.Net.BCrypt.HashPassword(registerUserDTO.Password),
            registerUserDTO.Email
        ));

        await _appDbContext.SaveChangesAsync();

        return new RegistrationResponse(true, "Registration Completed.");
    }
}
