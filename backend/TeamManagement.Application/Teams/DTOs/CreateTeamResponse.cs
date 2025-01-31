using TeamManagementSystem.Domain.Models;

namespace TeamManagementSystem.Application.Teams.DTOs;

public record CreateTeamResponse(bool Flag, string Message = null!, TeamEntity team = null!);

