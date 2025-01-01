using System;
using Microsoft.EntityFrameworkCore;
using TeamManagementSystem.Domain.Models;

namespace TeamManagementSystem.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity>? Users { get; set;}

}
