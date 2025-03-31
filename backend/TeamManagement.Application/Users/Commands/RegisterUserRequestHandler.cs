using System.ComponentModel.DataAnnotations;
using MediatR;
using TeamManagementSystem.Application.Common.Interfaces;
using TeamManagementSystem.Application.Common.Behaviours;
using TeamManagementSystem.Application.Common.Exceptions;
using TeamManagementSystem.Application.DTOs;
using TeamManagementSystem.Application.Interfaces;
using TeamManagementSystem.Domain.Models;

namespace TeamManagementSystem.Application.Users.Services;

public class RegisterUserRequest : IRequest<RegistrationResponse>, ISkipValidation
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

public class RegisterUserRequestHandler : IRequestHandler<RegisterUserRequest, RegistrationResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthenticate _authenticate;

    public RegisterUserRequestHandler(IUserRepository userRepository, IAuthenticate authenticate) {
        _userRepository = userRepository;
        _authenticate = authenticate;
    }

    public async Task<RegistrationResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        //var getUser = await FindUserByEmail(registerUserDTO.Email!);
        var existingUser = await _userRepository.FindUserByEmailAsync(request.Email!);

        if (existingUser != null) {
            throw new UserAlreadyExistsException(request.Email!);
        }

        var newUser = new UserEntity(
            Guid.NewGuid(),
            request.FirstName,
            request.LastName,
            request.UserName,
            request.Role,
            null,
            DateTime.UtcNow,
            null,
            _authenticate.HashPassword(request.Password!),
            request.Email
        );

        await _userRepository.AddUserAsync(newUser);

        return new RegistrationResponse(true, "Registration Completed.");
    }
}
