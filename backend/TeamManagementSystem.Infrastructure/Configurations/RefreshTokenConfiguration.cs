using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamManagementSystem.Domain.Models;

namespace TeamManagementSystem.Infrastructure.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
{
    public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder
        .Property(x => x.Token)
        .HasMaxLength(200);
    
        builder
        .HasIndex(x => x.Token)
        .IsUnique();

        builder
        .HasOne(x => x.User)
        .WithMany()
        .HasForeignKey(x => x.UserId);
    }
}
