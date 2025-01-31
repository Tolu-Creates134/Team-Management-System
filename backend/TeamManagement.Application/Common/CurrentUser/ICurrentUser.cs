using System;

namespace TeamManagementSystem.Application.Common.CurrentUser;

/// <summary>
/// Interface to represent the current logged in user
/// </summary>
public interface ICurrentUser
{
    /// <summary>
    /// Users user name
    /// </summary>
    public string? UserName { get; }

    /// <summary>
    /// Users email
    /// </summary>
    public string? Email { get; }

    /// <summary>
    /// Users Id 
    /// </summary>
    string? Id { get; }

    /// <summary>
    /// Users role 
    /// </summary>
    string? Role { get; }

}
