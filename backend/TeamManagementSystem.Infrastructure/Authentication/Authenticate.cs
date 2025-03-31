using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TeamManagementSystem.Application.Common.Interfaces;
using TeamManagementSystem.Domain.Models;
using TeamManagementSystem.Infrastructure.Configurations;

namespace TeamManagementSystem.Application.Common.Authentication;
public class Authenticate : IAuthenticate
{
    private readonly IConfiguration _configuration;

    private readonly AppDbContext _appDbContext;

    public Authenticate(IConfiguration configuration, AppDbContext appDbContext) {
        _configuration = configuration;
        _appDbContext = appDbContext;
    }

    public bool CheckPassword(string loginPassword, string DBPassword)
    {
        bool checkPassword = BCrypt.Net.BCrypt.Verify(loginPassword, DBPassword);

        return checkPassword;
    }

    public string GenerateRefreshtoken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }

    public string GenerateToken(UserEntity user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var userClaims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.GivenName, user.FirstName!),
            new Claim(ClaimTypes.Surname, user.LastName!),   
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Role, user.Role!)
        };

        var tokenDescriptor = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: userClaims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    public async Task AddRefreshToken (RefreshTokenEntity refreshToken)
    {
        _appDbContext.Refreshtokens!.Add(refreshToken);
        await _appDbContext.SaveChangesAsync();
    }

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public async Task<RefreshTokenEntity?> GetRefreshToken(string refreshToken)
    {
        var token = await _appDbContext.Refreshtokens!
        .Include(r => r.User)
        .FirstOrDefaultAsync(r => r.Token == refreshToken);

        return token;
    }

    public async Task UpdateRefreshToken(RefreshTokenEntity refreshToken)
    {
        _appDbContext.Refreshtokens!.Update(refreshToken);
        await _appDbContext.SaveChangesAsync();
        
    }
}