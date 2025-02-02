using TeamManagementSystem.Domain.Models.Team;

namespace TeamManagementSystem.Application.Teams.DTOs;

public record CreateTeamResponse(bool Flag, string Message = null!, TeamEntity team = null!);

