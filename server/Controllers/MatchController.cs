using Microsoft.AspNetCore.Mvc;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Service.Repositories;

namespace ping_Map_Play_pong.Controllers;

[ApiController]
[Route("api/[controller]")]

public class MatchController : ControllerBase
{
    private readonly ILogger<MatchController> _logger;
    private readonly IMatchRepository _matchRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITableRepository _tableRepository;

    public MatchController(ILogger<MatchController> logger, IMatchRepository matchRepository, IUserRepository userRepository, ITableRepository tableRepository)
    {
        _logger = logger;
        _matchRepository = matchRepository;
        _userRepository = userRepository;
        _tableRepository = tableRepository;
    }

    [HttpGet(Name = "matches")]
    public ActionResult<IEnumerable<Match>> GetAll()
    {
        try
        {
            var res = _matchRepository.GetAll().ToList();
            
            if (res.Count == 0) return NotFound("matches table is empty");
            
            return Ok(res);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("something went wrong");
        }
    }

    [HttpGet("matches/{matchId}")]
    public ActionResult<Match> GetById(int matchId)
    {
        try
        {
            var res = _matchRepository.GetById(matchId);
            
            if (res == null) return NotFound($"match with id:{matchId} not exist in DB");
            
            return Ok(res);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("something went wrong");
        }
    }
    
    [HttpGet("matches/table/{tableId}")]
    public ActionResult<IEnumerable<Match>> GetByTableId(int tableId)
    {
        try
        {
            var res = _matchRepository.GetByTableId(tableId).ToList();
            
            if (res.Count == 0) return NotFound($"match with id:{tableId} not exist in DB");
            
            return Ok(res);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("something went wrong");
        }
    }
    
    [HttpGet("matches/user/{userId}")]
    public ActionResult<IEnumerable<Match>> GetByUserId(int userId)
    {
        try
        {
            var res = _matchRepository.GetByUserId(userId).ToList();
            
            if (res.Count == 0) return NotFound($"match with userId:{userId} not exist in DB");
            
            return Ok(res);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("something went wrong");
        }
    }
    
    [HttpGet("matches/players/{player1Id}&{player2Id}")]
    public ActionResult<IEnumerable<Match>> GetByUserId(int player1Id, int player2Id)
    {
        try
        {
            var res = _matchRepository.GetByPlayer1IdAndPlayer2Id(player1Id, player2Id).ToList();
            
            if (res.Count == 0) return NotFound($"match between player:{player1Id} and player:{player2Id} not exist in DB");
            
            return Ok(res);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("something went wrong");
        }
    }
    
    [HttpGet("matches/date/{date}")]
    public ActionResult<IEnumerable<Match>> GetByTableId(DateTime date)
    {
        try
        {
            var res = _matchRepository.GetByDate(date).ToList();
            
            if (res.Count == 0) return NotFound($"match with date:{date} not exist in DB");
            
            return Ok(_matchRepository.GetByDate(date));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("something went wrong");
        }
    }
    
    [HttpPost("matches/add")]
    public ActionResult<string> Post(int tableId, int player1Id, int player2Id, DateTime startTime, DateTime endTime)
    {
        try
        {
            var table = _tableRepository.GetByTableId(tableId);
            if (table == null) return NotFound($"table with date:{tableId} not exist in DB");
            var player1 = _userRepository.GetById(player1Id);
            var player2 = _userRepository.GetById(player2Id);
            
            var newMatch = new Match
            {
                TableId = table.Id,
                Player1 = player1,
                Player2 = player2,
                StartDate = startTime,
                EndDate = endTime
            };
            _matchRepository.Add(newMatch);
            
            return Ok("success added new match");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("something went wrong");
        }
    }

    [HttpPatch("matches/id/{matchId}")]
    public ActionResult<string> Update(int matchId)
    {
        try
        {
            var match = _matchRepository.GetById(matchId);
            
            _matchRepository.Update(match);
            return Ok("successful update");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"match with id:{matchId} not exist in DB");
        }
    }

    [HttpDelete("matches/id/{matchId}")]
    public ActionResult<string> Delete(int matchId)
    {
        try
        {
            var match = _matchRepository.GetById(matchId);
            
            _matchRepository.Delete(match);
            return Ok("successful delete");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"match with id:{matchId} not exist in DB");
        }
    }
}