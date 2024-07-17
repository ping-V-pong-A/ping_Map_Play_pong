using Microsoft.AspNetCore.Mvc;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.RequestModels;
using ping_Map_Play_pong.Service;
using ping_Map_Play_pong.Service.Repositories;

namespace ping_Map_Play_pong.Controllers;

[ApiController]
[Route("api/matches")]

public class MatchController : ControllerBase
{
    private readonly ILogger<MatchController> _logger;
    private readonly IMatchService _matchService;

    public MatchController(ILogger<MatchController> logger, IMatchService matchService)
    {
        _logger = logger;
        _matchService = matchService;
    }

    [HttpGet(Name = "matches")]
    public ActionResult<IEnumerable<Match>> GetAll()
    {
        try
        {
            var res = _matchService.GetAll().ToList();
            
            if (res.Count == 0) return NotFound("matches table is empty");
            
            return Ok(res);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("something went wrong");
        }
    }

    [HttpGet("{matchId}")]
    public ActionResult<Match> GetById(int matchId)
    {
        try
        {
            var res = _matchService.GetById(matchId);
            
            if (res == null) return NotFound($"match with id:{matchId} not exist in DB");
            
            return Ok(res);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("something went wrong");
        }
    }
    
    [HttpGet("user/{userId}")]
    public ActionResult<IEnumerable<Match>> GetByUserId(int userId)
    {
        try
        {
            return Ok(_matchService.GetByUserId(userId));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("something went wrong");
        }
    }
    
    [HttpGet("players/{player1Id}&{player2Id}")]
    public ActionResult<IEnumerable<Match>> GetByPlayersId(int player1Id, int player2Id)
    {
        try
        {
            return Ok(_matchService.GetByPlayersId(player1Id, player2Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("something went wrong");
        }
    }
    
    [HttpGet("date/{date}")]
    public ActionResult<IEnumerable<Match>> GetByDate(DateTime date)
    {
        try
        {
           return Ok(_matchService.GetByDate(date));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("something went wrong");
        }
    }
    
    [HttpPost("add")]
    public ActionResult<string> Post([FromBody] MatchRequest request)
    {
        try
        {
            _matchService.PostToDb(request);
            _logger.LogInformation("success added new match");
            return Ok("success added new match");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("something went wrong");
        }
    }

    [HttpPatch("update/{matchId}")]
    public ActionResult<string> Update(int matchId)
    {
        try
        {
            var match = _matchService.GetById(matchId);
            
            if (match == null) return NotFound($"match with id:{matchId} not exist in DB");
            
            _matchService.Update(match);
            return Ok("successful update");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("something went wrong");
        }
    }

    [HttpDelete("delete/{matchId}")]
    public ActionResult<string> Delete(int matchId)
    {
        try
        {
            var match = _matchService.GetById(matchId);
            
            if (match == null) return NotFound($"match with id:{matchId} not exist in DB");
            
            _matchService.DeleteFromDb(match);
            return Ok("successful delete");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("something went wrong");
        }
    }
}