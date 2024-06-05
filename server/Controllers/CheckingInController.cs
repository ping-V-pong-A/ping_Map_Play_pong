using Microsoft.AspNetCore.Mvc;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Service.Repositories;

namespace ping_Map_Play_pong.Controllers;

[ApiController]
[Route("api/[controller]")]

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

    [HttpGet(Name = "checkingIns")]
    public ActionResult<IEnumerable<CheckingIn>> GetAll()
    {
        try
        {
            return Ok(_checkingInRepository.GetAll());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound("checkingIns table is empty");
        }
    }

    [HttpGet("checkingIns/table.id/{tableId}")]
    public ActionResult<IEnumerable<CheckingIn>> GetByTableId(int tableId)
    {
        try
        {
            return Ok(_checkingInRepository.GetByTableId(tableId));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"checkingIn with tableId:{tableId} not exist in DB");
        }
    }
    
    [HttpGet("checkingIns/user.id/{userId}")]
    public ActionResult<IEnumerable<CheckingIn>> GetByUserId(int userId)
    {
        try
        {
            return Ok(_checkingInRepository.GetByUserId(userId));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"checkingIn with userId:{userId} not exist in DB");
        }
    }
    
    [HttpGet("checkingIns/user.id/date")]
    public ActionResult<IEnumerable<CheckingIn>> GetByUserIdAndDate(int userId, DateTime date)
    {
        try
        {
            return Ok(_checkingInRepository.GetByUserIdAndDate(userId, date));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"checkingIn with userId:{userId} on this date:{date} not exist in DB");
        }
    }
    
    [HttpGet("checkingIns/user.id/startdate")]
    public ActionResult<IEnumerable<CheckingIn>> GetByUserIdAndStartDateTime(int userId, DateTime date)
    {
        try
        {
            return Ok(_checkingInRepository.GetByUserIdAndStartDateTime(userId, date));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"checkingIn with userId:{userId} on this date:{date} not exist in DB");
        }
    }
    
    [HttpPost("checkingIns/add")]
    public ActionResult<string> Post(int userId, int tableId, DateTime startTime, DateTime endTime)
    {
        try
        {
            var user = _userRepository.GetById(userId);
            var table = _tableRepository.GetByTableId(tableId);
            
            var newCheckingIn = new CheckingIn
            {
                User = user,
                Table = table,
                StartDate = startTime,
                EndDate = endTime
            };
            _checkingInRepository.Add(newCheckingIn);
            
            return Ok("success added new checkingIn");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("un success added new checkingIn");
        }
    }

    [HttpPatch("checkingIns/id/{checkingInId}")]
    public ActionResult<string> Update(int checkingInId)
    {
        try
        {
            var checkingIn = _checkingInRepository.GetById(checkingInId);
            
            _checkingInRepository.Update(checkingIn);
            return Ok("successful update");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"checkingIn with id:{checkingInId} not exist in DB");
        }
    }

    [HttpDelete("checkingIns/id/{checkingInId}")]
    public ActionResult<string> Delete(int checkingInId)
    {
        try
        {
            var checkingIn = _checkingInRepository.GetById(checkingInId);
            
            _checkingInRepository.Delete(checkingIn);
            return Ok("successful delete");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"checkingIn with id:{checkingInId} not exist in DB");
        }
    }
}