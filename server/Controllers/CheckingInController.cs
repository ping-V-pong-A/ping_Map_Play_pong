using Microsoft.AspNetCore.Mvc;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.Exceptions;
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
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse(e.Message);
        }
    }
    
    [HttpGet("user/{userId}")]
    public ActionResult<IEnumerable<CheckingIn>> GetByUserId(int userId)
    {
        try
        {
            return Ok( _checkingInService.GetByUserId(userId));
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse(e.Message);
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
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse(e.Message);
        }
    }

    [HttpPatch("update/{checkingInId}")]
    public ActionResult<string> Update(int checkingInId, [FromBody] CheckInRequest request)
    {
        try
        {
            _checkingInService.Update(checkingInId, request);
            
            return Ok("successful update");
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse(e.Message);
        }
    }

    [HttpDelete("delete/{checkingInId}")]
    public ActionResult<string> Delete(int checkingInId)
    {
        try
        {
            _checkingInService.DeleteFromDb(checkingInId);
            return Ok("successful delete");
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse(e.Message);
        }
    }
}