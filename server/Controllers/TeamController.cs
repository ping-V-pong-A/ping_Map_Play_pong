using Microsoft.AspNetCore.Mvc;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Service.Repositories;

namespace ping_Map_Play_pong.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TeamController : ControllerBase
{
    private readonly ILogger<TeamController> _logger;
    private readonly ITeamRepository _teamRepository;
    private readonly IUserRepository _userRepository;

    public TeamController(ILogger<TeamController> logger, ITeamRepository teamRepository, IUserRepository userRepository)
    {
        _logger = logger;
        _teamRepository = teamRepository;
        _userRepository = userRepository;
    }

    [HttpGet(Name = "teams")]
    public ActionResult<IEnumerable<Team>> GetAll()
    {
        try
        {
            return Ok(_teamRepository.GetAll());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound("teams table is empty");
        }
    }

    [HttpGet("teams/id/{userId}")]
    public ActionResult<Team> GetByUserId(int userId)
    {
        try
        {
            return Ok(_teamRepository.GetByUserId(userId));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"team with id:{userId} not exist in DB");
        }
    }
    
    [HttpGet("teams/players")]
    public ActionResult<Team> GetByPlayersId(int player1Id, int player2Id)
    {
        try
        {
            return Ok(_teamRepository.GetByPlayersId(player1Id, player2Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"team with players:{player1Id} and {player2Id} not exist in DB");
        }
    }
    
    [HttpPost("teams/add")]
    public ActionResult<string> Post(int player1Id, int player2Id)
    {
        try
        {
            var player1 = _userRepository.GetById(player1Id);
            var player2 = _userRepository.GetById(player2Id);
                
            var newTeam = new Team
            {
                Player1 = player1,
                Player2 = player2
            };
            _teamRepository.Add(newTeam);
            
            return Ok("success added new team");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("un success added new team");
        }
    }

    [HttpPatch("teams/id/{teamId}")]
    public ActionResult<string> Update(int teamId)
    {
        try
        {
            var team = _teamRepository.GetById(teamId);
            
            _teamRepository.Update(team);
            return Ok("successful update");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"team with id:{teamId} not exist in DB");
        }
    }

    [HttpDelete("teams/id/{teamId}")]
    public ActionResult<string> Delete(int teamId)
    {
        try
        {
            var team = _teamRepository.GetById(teamId);
            
            _teamRepository.Delete(team);
            return Ok("successful delete");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"team with id:{teamId} not exist in DB");
        }
    }
}