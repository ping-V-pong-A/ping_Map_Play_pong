using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    private readonly UserManager<IdentityUser> _userManager;

    public UserController(ILogger<UserController> logger, IUserRepository userRepository, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _userRepository = userRepository;
        _userManager = userManager;
    }

    [HttpGet(Name = "users"), ] 
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
    
    [HttpGet("users/name/{userName}"),Authorize(Roles = "Admin")]
    public ActionResult<User> GetByName(string userName)
    {
        try
        {
            return Ok(_userRepository.GetByNameAsync(userName));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return NotFound($"user with id:{userName} not exist in DB");
        }
    }
    
    /*
    [HttpPost("users/register")]
    public ActionResult<string> Post(string userName, string email, string password)
    {
        try
        {
            var newUser = new User
            {
                Name = userName,
      
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
    }*/

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
    public async Task<IActionResult> Delete(int userId)
    {
        try
        {
            var user = _userRepository.GetById(userId);
            if (user == null)
            {
                return NotFound($"User with ID:{userId} not found in database");
            }

            var identityUser = await _userManager.FindByEmailAsync(user.IdentityUserEmail);
     
            if (identityUser != null)
            {
                var result = await _userManager.DeleteAsync(identityUser);
          
                if (!result.Succeeded)
                {
                    _logger.LogError("Failed to delete identity user: " + result.Errors.FirstOrDefault()?.Description);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete identity user");
                }
            }
            else
            {
                _logger.LogWarning($"Identity user not found for user ID: {userId}");
            }
            

            return Ok("User successfully deleted");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while deleting user");
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An error occurred while processing the request");
        }
    }


    
    
}