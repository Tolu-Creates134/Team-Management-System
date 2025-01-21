using TeamManagementSystem.Domain.Models;

namespace TeamManagementSystem.Domain.Interfaces;

public interface ITeamRepository
{
    Task<TeamEntity?> GetTeamByIdAsync(Guid id);
    Task<IEnumerable<TeamEntity?>> GetAllTeamsAsync();
    Task CreateTeamAsync(TeamEntity team);
    Task UpdateTeamAsync(TeamEntity team);
    Task DeleteTeamAsync(Guid id);
}
