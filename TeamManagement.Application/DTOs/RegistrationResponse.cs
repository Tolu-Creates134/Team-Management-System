using System;

namespace TeamManagementSystem.Application.DTOs;

public record RegistrationResponse (bool Flag, string Message = null!);
