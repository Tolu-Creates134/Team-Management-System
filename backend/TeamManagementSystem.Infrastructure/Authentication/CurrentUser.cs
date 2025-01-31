using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TeamManagementSystem.Application.Common.CurrentUser;

namespace TeamManagementSystem.Infrastructure.Authentication;

public record CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserName => GetClaim(ClaimTypes.Name);

    public string? Email => GetClaim(ClaimTypes.Email);

    public string? Id => GetClaim(ClaimTypes.NameIdentifier);

    public string? Role => GetClaim(ClaimTypes.Role);


    /// <summary>
    /// Gets the claim value from the current user token.
    /// </summary>
    /// <param name="type">The claim type</param>
    /// <returns>The claim value</returns>
    public string? GetClaim(string type)
    {
        return 
        _httpContextAccessor
        .HttpContext
        ?.User
        .Claims
        .FirstOrDefault(x => x.Type == type)
        ?.Value;
    }

}
