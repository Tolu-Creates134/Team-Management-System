using System;
using TeamManagementSystem.Domain.Models;

namespace TeamManagementSystem.Application.Interfaces;

public interface IUserRepository
{
    Task <UserEntity?> FindUserByEmailAsync(string email);

    Task AddUserAsync(UserEntity user);

    Task UpdateUserAsync(UserEntity user);

}
