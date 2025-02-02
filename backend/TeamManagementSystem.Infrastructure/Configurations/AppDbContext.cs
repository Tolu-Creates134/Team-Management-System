using Microsoft.EntityFrameworkCore;
using TeamManagementSystem.Domain.Models;
using TeamManagementSystem.Domain.Models.Team;

namespace TeamManagementSystem.Infrastructure.Configurations;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{

    public DbSet<UserEntity>? Users { get; set; }
    public DbSet<TeamEntity>? Teams { get; set; }

    public DbSet<RefreshTokenEntity>? Refreshtokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppContext).Assembly);

        base.OnModelCreating(modelBuilder);

        // Configure primary key for user   
        modelBuilder.Entity<UserEntity>()
        .HasKey( x => x.Id);
        
        modelBuilder.Entity<TeamEntity>()
        .HasKey( x => x.Id);

        modelBuilder.Entity<TeamEntity>()
        .HasOne(t => t.TeamOwner) // Each Team has one User as the owner
        .WithMany(u => u.Teams)  // Each User can own many Teams
        .HasForeignKey(t => t.OwnerId) // TeamOwnerId is the Foreign Key
        .OnDelete(DeleteBehavior.Cascade); // Cascade delete: deleting a User deletes their Teams
    }

}
