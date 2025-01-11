using System;
using System.ComponentModel.DataAnnotations;
using MediatR;
using TeamManagementSystem.Application.DTOs;

namespace TeamManagementSystem.Application.Users.Commands;

public class LoginUserCommand : IRequest<LoginResponse>
{
    [Required, EmailAddress]
    public string Email { get; set;} = string.Empty;
    
    [Required]
    public string Password { get; set;} = string.Empty;
}

