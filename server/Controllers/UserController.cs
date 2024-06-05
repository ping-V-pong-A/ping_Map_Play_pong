using Microsoft.AspNetCore.Mvc;
using ping_Map_Play_pong.Model;
using ping_Map_Play_pong.Service.Repositories;

namespace ping_Map_Play_pong.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _userRepository;

    public UserController(ILogger<UserController> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    [HttpGet(Name = "users")]
    public ActionResult<IEnumerable<User>> GetAll()
    {
        try
        {
            return Ok(_userRepository.GetAll());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound("users table is empty");
        }
    }

    [HttpGet("users/id/{userId}")]
    public ActionResult<User> GetById(int userId)
    {
        try
        {
            return Ok(_userRepository.GetById(userId));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"user with id:{userId} not exist in DB");
        }
    }
    
    [HttpGet("users/name/{userName}")]
    public ActionResult<User> GetByName(string userName)
    {
        try
        {
            return Ok(_userRepository.GetByName(userName));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"user with id:{userName} not exist in DB");
        }
    }
    
    [HttpPost("users/register")]
    public ActionResult<string> Post(string userName, string email, string password)
    {
        try
        {
            var newUser = new User
            {
                Name = userName,
                Email = email,
                Password = password,
                Rank = 0
            };
            
            _userRepository.Add(newUser);
            
            return Ok("success registering");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("un success registering");
        }
    }

    [HttpPatch("users/id/{userId}")]
    public ActionResult<string> Update(int userId)
    {
        try
        {
            var user = _userRepository.GetById(userId);
            
            _userRepository.Update(user);
            return Ok("successful update");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"user with id:{userId} not exist in DB");
        }
    }
    
    [HttpDelete("users/id/{userId}")]
    public ActionResult<string> Delete(int userId)
    {
        try
        {
            var user = _userRepository.GetById(userId);
            
            _userRepository.Delete(user);
            return Ok("successful delete");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"user with id:{userId} not exist in DB");
        }
    }
}