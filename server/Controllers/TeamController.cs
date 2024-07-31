using Microsoft.AspNetCore.Mvc;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.Exceptions;
using ping_Map_Play_pong.Service;

namespace ping_Map_Play_pong.Controllers;

[ApiController]
[Route("api/teams")]
public class TeamController : ControllerBase
{
    private readonly ILogger<TeamController> _logger;
    private readonly ITeamService _teamService;

    public TeamController(ILogger<TeamController> logger, ITeamService teamService)
    {
        _logger = logger;
        _teamService = teamService;
    }

    [HttpGet(Name = "teams")]
    public ActionResult<IEnumerable<Team>> GetAll()
    {
        try
        {
            return Ok(_teamService.GetAll());
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
        }
    }

    [HttpGet("{userId}")]
    public ActionResult<IEnumerable<Team>> GetByUserId(int userId)
    {
        try
        {
            return Ok(_teamService.GetByUserId(userId));
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
        }
    }
    
    [HttpGet("players/{player1Id}/{player2Id}")]
    public ActionResult<Team> GetByPlayersId(int player1Id, int player2Id)
    {
        try
        {
            return Ok(_teamService.GetByPlayersId(player1Id, player2Id));
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
        }
    }
    
    [HttpPost("add")]
    public ActionResult<string> Post(int player1Id, int player2Id)
    {
        try
        {
            _teamService.PostToDb(player1Id, player2Id);
            return Ok("success added new team");
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
        }
    }

    [HttpDelete("delete/{teamId}")]
    public ActionResult<string> Delete(int teamId)
    {
        try
        {
            _teamService.DeleteFromDb(teamId);
            return Ok("successful delete");
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
        }
    }
}