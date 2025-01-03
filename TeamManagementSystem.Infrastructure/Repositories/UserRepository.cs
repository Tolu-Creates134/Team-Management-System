using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using TeamManagementSystem.Application.DTOs;
using TeamManagementSystem.Application.Interfaces;
using TeamManagementSystem.Domain.Models;
using TeamManagementSystem.Infrastructure.Data;


namespace TeamManagementSystem.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;

    private readonly IConfiguration _configuration;

    public UserRepository(AppDbContext appDbContext, IConfiguration configuration) {
        _appDbContext = appDbContext;
        _configuration = configuration;
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

    private string GenerateJWTToken(UserEntity user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var userClaims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FirstName!),
            new Claim(ClaimTypes.Name, user.LastName!),   
            new Claim(ClaimTypes.Email, user.Email!),
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: userClaims,
            expires: DateTime.Now.AddDays(5),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
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
