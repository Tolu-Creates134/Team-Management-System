using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TeamManagementSystem.Application.Interfaces;
using TeamManagementSystem.Domain.Models;
using TeamManagementSystem.Infrastructure.Data;


namespace TeamManagementSystem.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _appDbContext;

    public UserRepository(AppDbContext appDbContext) {
        _appDbContext = appDbContext;
    }

    public async Task<UserEntity?> FindUserByEmailAsync(string email)
    {
        return await _appDbContext.Users!.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async  Task AddUserAsync(UserEntity user)
    {
        await _appDbContext.Users!.AddAsync(user);
        await _appDbContext.SaveChangesAsync();
    }
}
