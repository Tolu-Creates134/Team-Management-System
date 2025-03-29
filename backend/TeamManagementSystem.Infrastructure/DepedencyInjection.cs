using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TeamManagementSystem.Application.Common.Authentication;
using TeamManagementSystem.Application.Common.CurrentUser;
using TeamManagementSystem.Application.Interfaces;
using TeamManagementSystem.Domain.Interfaces;
using TeamManagementSystem.Infrastructure.Authentication;
using TeamManagementSystem.Infrastructure.Configurations;
using TeamManagementSystem.Infrastructure.Repositories;

namespace TeamManagementSystem.Infrastructure;

public static class DepedencyInjection
{
    public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration) {

        services.AddDbContext<AppDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString);
        });

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options => 
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
            };
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<IAuthenticate, Authenticate>();
        services.AddScoped<ICurrentUser, CurrentUser>();

        return services;
    }

}
