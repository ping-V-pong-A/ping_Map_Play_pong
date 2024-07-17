using Microsoft.AspNetCore.Mvc;
using ping_Map_Play_pong.Model;
using ping_Map_Play_pong.Model.RequestModels;
using ping_Map_Play_pong.Model.ResponseModels;
using ping_Map_Play_pong.Service;

namespace ping_Map_Play_pong.Controllers;

[ApiController]
[Route("api/users")]

public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet(Name = "users"),]
    public ActionResult<IEnumerable<User>> GetAll()
    {
        try
        {
            var users = _userService.GetAll();
            
            var respUsers = users.Select(user => new UserResponse
            {
                Id = user.Id,
                RegistrationDate = user.RegistrationDate,
                CheckedInTables = user.CheckedInTables.ToList(),
                Rank = user.Rank
            }).ToList();

            return Ok(respUsers);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("something went wrong");
        }
    }
    
    [HttpGet("{userId}")]
    public ActionResult<User> GetById(int userId)
    {
        try
        {
            return Ok(_userService.GetById(userId));
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("something went wrong");
        }
    }

    [HttpPatch("update/{userId}")]
    public ActionResult<string> Update(int userId, [FromBody] UserRequest request)
    {
        try
        {
            var user = _userService.GetById(userId);
            
            if (user == null)
            {
                return NotFound($"User with ID:{userId} not found in database");
            }
            
            // TODO
            // _userService.Update(request);
            return Ok("successful update");
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("something went wrong");
        }
    }

    [HttpDelete("delete/{userId}")]
    public IActionResult Delete(int userId)
    {
        try
        {
            var user = _userService.GetById(userId);
            
            if (user == null)
            {
                return NotFound($"User with ID:{userId} not found in database");
            }
            
            _userService.Delete(user);

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