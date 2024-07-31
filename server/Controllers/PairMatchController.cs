using Microsoft.AspNetCore.Mvc;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.Exceptions;
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
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
        }
    }
    
    [HttpGet("date/{date}")]
    public ActionResult<IEnumerable<PairMatch>> GetByDate(DateTime date)
    {
        try
        {
            return Ok(_pairMatchService.GetByDate(date));
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
        }
    }
    
    [HttpGet("{pairMatchId}")]
    public ActionResult<PairMatch> GetById(int pairMatchId)
    {
        try
        {
            return Ok(_pairMatchService.GetById(pairMatchId));
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
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
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
        }
    }

    [HttpPatch("update/{pairMatchId}")]
    public ActionResult<string> Update(int pairMatchId, [FromBody] PairMatchRequest request)
    {
        try
        { 
            _pairMatchService.Update(pairMatchId, request);
            return Ok("successful update");
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
        }
    }

    [HttpDelete("delete/{pairMatchId}")]
    public ActionResult<string> Delete(int pairMatchId)
    {
        try
        {
            _pairMatchService.DeleteFromDb(pairMatchId);
            return Ok("successful delete");
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
        }
    }
}