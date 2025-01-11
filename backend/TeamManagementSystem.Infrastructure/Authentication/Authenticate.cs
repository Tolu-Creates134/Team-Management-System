using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TeamManagementSystem.Domain.Models;

namespace TeamManagementSystem.Application.Common.Authentication;
public class Authenticate : IAuthenticate
{
    private readonly IConfiguration _configuration;

    public Authenticate(IConfiguration configuration) {
        _configuration = configuration;
    }

    public bool checkPassword(string loginPassword, string DBPassword)
    {
        bool checkPassword = BCrypt.Net.BCrypt.Verify(loginPassword, DBPassword);

        return checkPassword;
    }

    public string generateToken(UserEntity user)
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

    public string hashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}