using Microsoft.AspNetCore.Mvc;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.RequestModels;
using ping_Map_Play_pong.Service;

namespace ping_Map_Play_pong.Controllers;

[ApiController]
[Route("api/check-ins")]

public class CheckingInController : ControllerBase
{
    private readonly ILogger<CheckingInController> _logger;
    private readonly ICheckingInService _checkingInService;

    public CheckingInController(ILogger<CheckingInController> logger, ICheckingInService checkingInService)
    {
        _logger = logger;
        _checkingInService = checkingInService;
    }

    [HttpGet(Name = "check-ins")]
    public ActionResult<IEnumerable<CheckingIn>> GetAll()
    {
        try
        {
            return Ok(_checkingInService.GetAll());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("something went wrong");
        }
    }
    
    [HttpGet("user/{userId}")]
    public ActionResult<IEnumerable<CheckingIn>> GetByUserId(int userId)
    {
        try
        {
            return Ok( _checkingInService.GetByUserId(userId));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("something went wrong");
        }
    }
    
    [HttpPost("add")]
    public ActionResult<string> Post([FromBody] CheckInRequest request)
    {
        try
        {
            _checkingInService.PostToDb(request);
            
            return Ok("success added new checkingIn");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("something went wrong");
        }
    }

    [HttpPatch("update/{checkingInId}")]
    public ActionResult<string> Update(int checkingInId)
    {
        try
        {
            var checkingIn = _checkingInService.GetById(checkingInId);
            
            if (checkingIn == null) return NotFound($"checkingIn with id:{checkingInId} not exist in DB");
            
            _checkingInService.Update(checkingIn);
            
            return Ok("successful update");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("something went wrong");
        }
    }

    [HttpDelete("delete/{checkingInId}")]
    public ActionResult<string> Delete(int checkingInId)
    {
        try
        {
            var checkingIn = _checkingInService.GetById(checkingInId);
            
            if (checkingIn == null) return NotFound($"checkingIn with id:{checkingInId} not exist in DB");
            
            _checkingInService.DeleteFromDb(checkingIn);
            return Ok("successful delete");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("something went wrong");
        }
    }
}