using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamManagementSystem.Domain.Models;

public class UserEntity
{

    public UserEntity (
        Guid id,
        string? firstName,
        string? lastName,
        string? userName,
        string? role,
        Guid? teamId,
        DateTime? createdDate,
        DateTime? updatedDate,
        string? passwordHash,
        string? email
    )
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        Role = role;
        TeamId = teamId;
        CreatedDate = createdDate;
        UpdatedDate = updatedDate;
        PasswordHash = passwordHash;
        Email = email;
    }

    public Guid Id { get; private set;}
    
    [Column(TypeName = "varchar(50)")]
    public string? FirstName { get; private set; }

    [Column(TypeName = "varchar(50)")]
    public string? LastName { get; private set; }

    [Column(TypeName = "varchar(50)")]
    public string? UserName { get;}

    [Column(TypeName = "varchar(50)")]
    public string? Role { get; private set; }

    public Guid? TeamId { get; private set; }

    [Column(TypeName = "datetime2")]
    public DateTime? CreatedDate  { get; private set; }

    [Column(TypeName = "datetime2")]
    public DateTime? UpdatedDate { get; private set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? PasswordHash { get; private set; }

    [Column(TypeName = "varchar(255)")]
    public string? Email { get; private set; }

}
