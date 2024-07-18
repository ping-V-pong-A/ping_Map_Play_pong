using Microsoft.AspNetCore.Mvc;
using ping_Map_Play_pong.Model.DataModels;
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
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound("teams table is empty");
        }
    }

    [HttpGet("{userId}")]
    public ActionResult<IEnumerable<Team>> GetByUserId(int userId)
    {
        try
        {
            return Ok(_teamService.GetByUserId(userId));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"team with id:{userId} not exist in DB");
        }
    }
    
    [HttpGet("players/{player1Id}/{player2Id}")]
    public ActionResult<Team> GetByPlayersId(int player1Id, int player2Id)
    {
        try
        {
            return Ok(_teamService.GetByPlayersId(player1Id, player2Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"team with players:{player1Id} and {player2Id} not exist in DB");
        }
    }
    
    [HttpPost("add")]
    public ActionResult<string> Post(int player1Id, int player2Id)
    {
        try
        {
            if (_teamService.GetByPlayersId(player1Id, player2Id) != null)
            {
                _logger.LogInformation("This team already exist");
                return BadRequest("This team already exist");
            }
            
            _teamService.PostToDb(player1Id, player2Id);
            
            return Ok("success added new team");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("un success added new team");
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
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"team with id:{teamId} not exist in DB");
        }
    }
}