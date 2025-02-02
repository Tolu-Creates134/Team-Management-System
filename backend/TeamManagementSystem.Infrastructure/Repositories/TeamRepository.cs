using Microsoft.EntityFrameworkCore;
using TeamManagementSystem.Domain.Interfaces;
using TeamManagementSystem.Domain.Models.Team;
using TeamManagementSystem.Infrastructure.Configurations;

namespace TeamManagementSystem.Infrastructure.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly AppDbContext _appDbContext;

    public TeamRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task CreateTeamAsync(TeamEntity team)
    {
        await _appDbContext.Teams!.AddAsync(team);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteTeamAsync(Guid id)
    {
        var team = await _appDbContext.Teams!.FindAsync(id);
            if (team != null)
            {
                _appDbContext.Teams.Remove(team);
                await _appDbContext.SaveChangesAsync();
            }
    }

    public async Task<IEnumerable<TeamEntity?>> GetAllTeamsAsync()
    {
        return await _appDbContext.Teams!.ToListAsync();
    }

    public async Task<TeamEntity?> GetTeamByIdAsync(Guid id)
    {
        return await _appDbContext.Teams!.FindAsync(id);
    }

    public async Task UpdateTeamAsync(TeamEntity team)
    {
        _appDbContext.Teams!.Update(team);
        await _appDbContext.SaveChangesAsync();
    }
}
