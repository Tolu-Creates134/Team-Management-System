using System;
using System.ComponentModel.DataAnnotations;

namespace TeamManagementSystem.Application.DTOs;

public class RegisterUserDTO
{
    [Required]
    public string? FirstName { get; set;}

    [Required]
    public string? LastName { get; set;}

    [Required]
    public string? UserName { get; set;}

    [Required, EmailAddress]
    public string? Email { get; set;}

    [Required, MinLength(6)]
    public string? Password { get; set;}

    [Required, Compare(nameof(Password))]
    public string? ConfirmPassword { get; set;}

    [Required]
    public string? Role { get; set;}

    public DateTime? CreatedDate { get; set;}

    public DateTime? UpdatedDate { get; set;}
}

public enum UserRole
{
    Admin,
    Coach,
    Player
}
