using Microsoft.AspNetCore.Mvc;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.Exceptions;
using ping_Map_Play_pong.Model.RequestModels;
using ping_Map_Play_pong.Service;
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
            return Ok(_matchService.GetAll());
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
        }
    }

    [HttpGet("{matchId}")]
    public ActionResult<Match> GetById(int matchId)
    {
        try
        {
            return Ok(_matchService.GetById(matchId));
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
        }
    }
    
    [HttpGet("user/{userId}")]
    public ActionResult<IEnumerable<Match>> GetByUserId(int userId)
    {
        try
        {
            return Ok(_matchService.GetByUserId(userId));
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
        }
    }
    
    [HttpGet("players/{player1Id}&{player2Id}")]
    public ActionResult<IEnumerable<Match>> GetByPlayersId(int player1Id, int player2Id)
    {
        try
        {
            return Ok(_matchService.GetByPlayersId(player1Id, player2Id));
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
        }
    }
    
    [HttpGet("date/{date}")]
    public ActionResult<IEnumerable<Match>> GetByDate(DateTime date)
    {
        try
        {
           return Ok(_matchService.GetByDate(date));
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
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
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
        }
    }

    [HttpPatch("update/{matchId}")]
    public ActionResult<string> Update(int matchId, [FromBody] MatchRequest request)
    {
        try
        {
            _matchService.Update(matchId, request);
            return Ok("successful update");
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
        }
    }

    [HttpDelete("delete/{matchId}")]
    public ActionResult<string> Delete(int matchId)
    {
        try
        {
            _matchService.DeleteFromDb(matchId);
            return Ok("successful delete");
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
        }
    }
}