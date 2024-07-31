using Microsoft.AspNetCore.Mvc;
using ping_Map_Play_pong.Model.Exceptions;
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
    public ActionResult<IEnumerable<UserResponse>> GetAll()
    {
        try
        {
            return Ok(_userService.GetAll());
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
        }
    }
    
    [HttpGet("{userId}")]
    public ActionResult<UserResponse> GetById(int userId)
    {
        try
        {
            return Ok(_userService.GetById(userId));
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
        }
    }

    [HttpPatch("update/{userId}")]
    public ActionResult<string> Update(int userId, [FromBody] UserRequest request)
    {
        try
        {
            _userService.Update(userId, request);
            return Ok("successful update");
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
        }
    }

    [HttpDelete("delete/{userId}")]
    public IActionResult Delete(int userId)
    {
        try
        {
            _userService.Delete(userId);
            return Ok("User successfully deleted");
        }
        catch (ExceptionBase e)
        {
            _logger.LogError(e, e.Message);
            return e.GetResponse($"{e.Message}");
        }
    }
}