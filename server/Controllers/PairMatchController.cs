using Microsoft.AspNetCore.Mvc;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.RequestModels;
using ping_Map_Play_pong.Service;
namespace ping_Map_Play_pong.Controllers;

[ApiController]
[Route("api/pair-matches")]

public class PairMatchController : ControllerBase
{
    private readonly ILogger<PairMatchController> _logger;
    private readonly IPairMatchService _pairMatchService;

    public PairMatchController(ILogger<PairMatchController> logger, IPairMatchService pairMatchService)
    {
        _logger = logger;
        _pairMatchService = pairMatchService;
    }

    [HttpGet(Name = "pair-matches")]
    public ActionResult<IEnumerable<PairMatch>> GetAll()
    {
        try
        {
            return Ok(_pairMatchService.GetAll());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound("pairPairMatches table is empty");
        }
    }
    
    [HttpGet("date/{date}")]
    public ActionResult<IEnumerable<PairMatch>> GetByDate(DateTime date)
    {
        try
        {
            return Ok(_pairMatchService.GetByDate(date));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"pairPairMatch with date:{date} not exist in DB");
        }
    }
    
    [HttpGet("{pairMatchId}")]
    public ActionResult<PairMatch> GetById(int pairMatchId)
    {
        try
        {
            return Ok(_pairMatchService.GetById(pairMatchId));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"pairPairMatch with id:{pairMatchId} not exist in DB");
        }
    }
    
    [HttpPost("add")]
    public ActionResult<string> Post([FromBody] PairMatchRequest request)
    {
        try
        {
            _pairMatchService.PostToDb(request);
            
            return Ok("success added new pairPairMatch");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("un success added new pairPairMatch");
        }
    }

    [HttpPatch("update/{pairMatchId}")]
    public ActionResult<string> Update(int pairMatchId)
    {
        try
        {
            var pairMatch = _pairMatchService.GetById(pairMatchId);
            
            if (pairMatch == null) return NotFound($"match with id:{pairMatchId} not exist in DB");
            
            _pairMatchService.Update(pairMatch);
            return Ok("successful update");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("something went wrong");
        }
    }

    [HttpDelete("delete/{pairMatchId}")]
    public ActionResult<string> Delete(int pairMatchId)
    {
        try
        {
            var pairMatch = _pairMatchService.GetById(pairMatchId);
            
            if (pairMatch == null) return NotFound($"match with id:{pairMatchId} not exist in DB");
            
            _pairMatchService.DeleteFromDb(pairMatch);
            return Ok("successful delete");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("something went wrong");
        }
    }
}