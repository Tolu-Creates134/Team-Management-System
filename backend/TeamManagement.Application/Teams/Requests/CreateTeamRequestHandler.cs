using MediatR;
using TeamManagementSystem.Application.Common.CurrentUser;
using TeamManagementSystem.Application.Teams.DTOs;
using TeamManagementSystem.Domain.Interfaces;
using TeamManagementSystem.Domain.Models;

namespace TeamManagementSystem.Application.Teams.Requests;

public class CreateTeamRequestHandler : IRequestHandler<CreateTeamRequest, CreateTeamResponse>
{
    private readonly ITeamRepository _teamRepository;

    private readonly ICurrentUser _currentUser;

    public CreateTeamRequestHandler(ITeamRepository teamRepository, ICurrentUser currentUser)
    {
        _teamRepository = teamRepository;
        _currentUser = currentUser;
    }

    public async Task<CreateTeamResponse> Handle(CreateTeamRequest request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "The request cannot be null.");
        }

        if (!Guid.TryParse(_currentUser.Id, out var ownerId))
        {
            throw new InvalidOperationException("Invalid User ID format.");
        }

        // Create new team entity
        var team = new TeamEntity(
            Guid.NewGuid(),
            request.name,
            request.coachName,
            ownerId,
            request.teamLogo,
            true
        );

        await _teamRepository.CreateTeamAsync(team);

        return new CreateTeamResponse(true, "Team is succesfully created", team);
    }

}
