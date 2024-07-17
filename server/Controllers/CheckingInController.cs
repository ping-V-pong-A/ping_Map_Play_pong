using Microsoft.AspNetCore.Mvc;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.RequestModels;
using ping_Map_Play_pong.Service.Repositories;

namespace ping_Map_Play_pong.Controllers;

[ApiController]
[Route("api/check-ins")]

public class CheckingInController : ControllerBase
{
    private readonly ILogger<CheckingInController> _logger;
    private readonly ICheckingInRepository _checkingInRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITableRepository _tableRepository;

    public CheckingInController(ILogger<CheckingInController> logger, ICheckingInRepository checkingInRepository, IUserRepository userRepository, ITableRepository tableRepository)
    {
        _logger = logger;
        _checkingInRepository = checkingInRepository;
        _userRepository = userRepository;
        _tableRepository = tableRepository;
    }

    [HttpGet(Name = "check-ins")]
    public ActionResult<IEnumerable<CheckingIn>> GetAll()
    {
        try
        {
            var res = _checkingInRepository.GetAll().ToList();
            
            if (res.Count == 0) return NotFound("checkingIns table is empty");
            
            return Ok(res);
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
            var res = _checkingInRepository.GetByUserId(userId).ToList();
            
            if (res.Count == 0) return NotFound($"checkingIn with userId:{userId} not exist in DB");
            
            return Ok(res);
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
            var user = _userRepository.GetById(request.UserId);
            var table = _tableRepository.GetByTableId(request.TableId);
            
            var newCheckingIn = new CheckingIn
            {
                UserId = user.Id,
                TableId = table.Id,
                StartDate = request.Start,
                EndDate = request.End
            };
            _checkingInRepository.Add(newCheckingIn);
            
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
            var checkingIn = _checkingInRepository.GetById(checkingInId);
            
            if (checkingIn == null) return NotFound($"checkingIn with id:{checkingInId} not exist in DB");
            
            _checkingInRepository.Update(checkingIn);
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
            var checkingIn = _checkingInRepository.GetById(checkingInId);
            
            if (checkingIn == null) return NotFound($"checkingIn with id:{checkingInId} not exist in DB");
            
            _checkingInRepository.Delete(checkingIn);
            return Ok("successful delete");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("something went wrong");
        }
    }
}