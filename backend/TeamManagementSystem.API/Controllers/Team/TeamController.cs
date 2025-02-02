using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeamManagementSystem.Application.Teams.DTOs;

namespace TeamManagementSystem.API.Controllers.Team;

[ApiController]
[Route("api/[controller]")]
public class TeamController : ControllerBase
{
    private readonly IMediator _mediator;

    public TeamController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<ActionResult<CreateTeamResponse>> CreateTeam(CreateTeamRequest request)
    {
        var result = await _mediator.Send(request);

        return Ok(result);
    }


}
