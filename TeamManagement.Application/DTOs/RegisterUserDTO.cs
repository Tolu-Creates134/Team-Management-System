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

    [Required]
    public string? Email { get; set;}

    [Required]
    public string? Password { get; set;}

    [Required, Compare(nameof(Password))]
    public string? ConfirmPassword { get; set;}

    [Required]
    public string? Role { get; set;}

    [Required]
    public string? CreatedDate { get; set;}

    [Required]
    public string? UpdatedDate { get; set;}
}
