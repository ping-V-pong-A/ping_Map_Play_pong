using Microsoft.AspNetCore.Mvc;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Service.Repositories;

namespace ping_Map_Play_pong.Controllers;

[ApiController]
[Route("api/[controller]")]

public class PairMatchController : ControllerBase
{
    private readonly ILogger<PairMatchController> _logger;
    private readonly IPairMatchRepository _pairPairMatchRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITableRepository _tableRepository;
    private readonly ITeamRepository _teamRepository;

    public PairMatchController(ILogger<PairMatchController> logger, IPairMatchRepository pairPairMatchRepository, IUserRepository userRepository, ITableRepository tableRepository, ITeamRepository teamRepository)
    {
        _logger = logger;
        _pairPairMatchRepository = pairPairMatchRepository;
        _userRepository = userRepository;
        _tableRepository = tableRepository;
        _teamRepository = teamRepository;
    }

    [HttpGet(Name = "pairPairMatches")]
    public ActionResult<IEnumerable<PairMatch>> GetAll()
    {
        try
        {
            return Ok(_pairPairMatchRepository.GetAll());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound("pairPairMatches table is empty");
        }
    }

    [HttpGet("pairPairMatches/{pairPairMatchId}")]
    public ActionResult<PairMatch> GetById(int pairPairMatchId)
    {
        try
        {
            return Ok(_pairPairMatchRepository.GetById(pairPairMatchId));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"pairPairMatch with id:{pairPairMatchId} not exist in DB");
        }
    }
    
    [HttpGet("pairPairMatches/table/{tableId}")]
    public ActionResult<IEnumerable<PairMatch>> GetByTableId(int tableId)
    {
        try
        {
            return Ok(_pairPairMatchRepository.GetByTableId(tableId));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"pairPairMatch with tableId:{tableId} not exist in DB");
        }
    }
    
    [HttpGet("pairPairMatches/user/{userId}")]
    public ActionResult<IEnumerable<PairMatch>> GetByUserId(int userId)
    {
        try
        {
            return Ok(_pairPairMatchRepository.GetByUserId(userId));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"pairPairMatch with userId:{userId} not exist in DB");
        }
    }
    
    [HttpGet("pairPairMatches/date/{date}")]
    public ActionResult<IEnumerable<PairMatch>> GetByTableId(DateTime date)
    {
        try
        {
            return Ok(_pairPairMatchRepository.GetByDate(date));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"pairPairMatch with date:{date} not exist in DB");
        }
    }
    
    [HttpPost("pairPairMatches/add")]
    public ActionResult<string> Post(int tableId, int team1Player1Id, int team1Player2Id, int team2Player1Id, int team2Player2Id, DateTime startTime, DateTime endTime)
    {
        try
        {
            var table = _tableRepository.GetByTableId(tableId);
            
            var team1 = new Team
            {
                Player1 = _userRepository.GetById(team1Player1Id),
                Player2 = _userRepository.GetById(team1Player2Id)
            };
                
            var team2 = new Team
            {
                Player1 = _userRepository.GetById(team2Player2Id),
                Player2 = _userRepository.GetById(team2Player1Id)
            };
            
            var newPairMatch = new PairMatch
            {
                Table = table,
                Team1 = team1,
                Team2 = team2,
                StartDate = startTime,
                EndDate = endTime
            };
            _pairPairMatchRepository.Add(newPairMatch);
            
            return Ok("success added new pairPairMatch");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("un success added new pairPairMatch");
        }
    }

    [HttpPatch("pairPairMatches/id/{pairPairMatchId}")]
    public ActionResult<string> Update(int pairPairMatchId)
    {
        try
        {
            var pairPairMatch = _pairPairMatchRepository.GetById(pairPairMatchId);
            
            _pairPairMatchRepository.Update(pairPairMatch);
            return Ok("successful update");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"pairPairMatch with id:{pairPairMatchId} not exist in DB");
        }
    }

    [HttpDelete("pairPairMatches/id/{pairPairMatchId}")]
    public ActionResult<string> Delete(int pairPairMatchId)
    {
        try
        {
            var pairPairMatch = _pairPairMatchRepository.GetById(pairPairMatchId);
            
            _pairPairMatchRepository.Delete(pairPairMatch);
            return Ok("successful delete");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"pairPairMatch with id:{pairPairMatchId} not exist in DB");
        }
    }
}