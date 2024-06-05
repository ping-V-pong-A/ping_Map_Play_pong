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
            return Ok(_matchRepository.GetAll());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound("matches table is empty");
        }
    }

    [HttpGet("matches/{matchId}")]
    public ActionResult<Match> GetById(int matchId)
    {
        try
        {
            return Ok(_matchRepository.GetById(matchId));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"match with id:{matchId} not exist in DB");
        }
    }
    
    [HttpGet("matches/table/{tableId}")]
    public ActionResult<IEnumerable<Match>> GetByTableId(int tableId)
    {
        try
        {
            return Ok(_matchRepository.GetByTableId(tableId));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"match with tableId:{tableId} not exist in DB");
        }
    }
    
    [HttpGet("matches/user/{userId}")]
    public ActionResult<IEnumerable<Match>> GetByUserId(int userId)
    {
        try
        {
            return Ok(_matchRepository.GetByUserId(userId));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"match with userId:{userId} not exist in DB");
        }
    }
    
    [HttpGet("matches/players/{player1Id}&{player2Id}")]
    public ActionResult<IEnumerable<Match>> GetByUserId(int player1Id, int player2Id)
    {
        try
        {
            return Ok(_matchRepository.GetByPlayer1IdAndPlayer2Id(player1Id, player2Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"match between player:{player1Id} and player:{player2Id} not exist in DB");
        }
    }
    
    [HttpGet("matches/date/{date}")]
    public ActionResult<IEnumerable<Match>> GetByTableId(DateTime date)
    {
        try
        {
            return Ok(_matchRepository.GetByDate(date));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"match with date:{date} not exist in DB");
        }
    }
    
    [HttpPost("matches/add")]
    public ActionResult<string> Post(int tableId, int player1Id, int player2Id, DateTime startTime, DateTime endTime)
    {
        try
        {
            var table = _tableRepository.GetByTableId(tableId);
            var player1 = _userRepository.GetById(player1Id);
            var player2 = _userRepository.GetById(player2Id);
            
            var newMatch = new Match
            {
                Table = table,
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
            return BadRequest("un success added new match");
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