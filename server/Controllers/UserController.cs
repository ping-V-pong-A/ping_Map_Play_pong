using Microsoft.AspNetCore.Mvc;
using ping_Map_Play_pong.Model;
using ping_Map_Play_pong.Service.Repositories;

namespace ping_Map_Play_pong.Controllers;

[ApiController]
[Route("api/users")]

public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _userRepository;

    public UserController(ILogger<UserController> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    [HttpGet("users/{id}")]
    public ActionResult<IEnumerable<User>> Get()
    {
        _logger.LogInformation("fetch from frontend");
        return Ok(_userRepository.GetAll());
    }
}