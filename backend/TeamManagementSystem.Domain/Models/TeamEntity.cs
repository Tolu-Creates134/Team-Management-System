using System.ComponentModel.DataAnnotations.Schema;

namespace TeamManagementSystem.Domain.Models;

public class TeamEntity
{
    public TeamEntity (
        Guid id,
        string name,
        string coachName,
        Guid ownerId,
        string teamLogo,
        bool isRegistered
    )
    {
        Id = id;
        Name = name;
        CoachName = coachName;
        OwnerId = ownerId;
        TeamLogo = teamLogo;
        IsRegistered = isRegistered;
    }

    public Guid Id { get; private set; }

    [Column(TypeName = "varchar(50)")]
    public string Name { get; private set;}

    [Column(TypeName = "varchar(50)")]
    public string CoachName { get; private set; }

    public Guid OwnerId { get; private set;}

    [Column(TypeName = "varchar(255)")]
    public string TeamLogo { get; private set;}

    [Column(TypeName = "bit")]
    public bool IsRegistered { get; private set;}

    // Navigation property for the Team's owner (many-to-one)
    public UserEntity? TeamOwner { get; set; }
}
