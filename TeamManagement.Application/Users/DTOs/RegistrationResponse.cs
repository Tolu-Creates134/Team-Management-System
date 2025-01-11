using System;
using TeamManagementSystem.Domain.Models;

namespace TeamManagementSystem.Application.DTOs;

public record RegistrationResponse (bool Flag, string Message = null!, UserEntity user = null!);
