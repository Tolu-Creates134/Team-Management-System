using System;

namespace TeamManagementSystem.Domain.Models;

public class RefreshTokenEntity
{
    public Guid Id { get; set;}

    public string? Token { get; set;}

    public Guid UserId { get; set;}

    public DateTime ExpiresOnUTC { get; set;}

    public UserEntity? User { get; set;}

}
