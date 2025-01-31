using MediatR;

namespace TeamManagementSystem.Application.Teams.DTOs;

public record class CreateTeamRequest 
(
    string name, // Name of the team
    string coachName, // Name of the coach (i.e users name)
    Guid OwnerId, // ID of the user creating the team
    string teamLogo, // Team logo converetd to string
    bool isRegistered 
): IRequest<CreateTeamResponse>;
