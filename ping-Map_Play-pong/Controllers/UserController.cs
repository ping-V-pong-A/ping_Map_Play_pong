using Microsoft.AspNetCore.Mvc;
using ping_Map_Play_pong.Model;
using ping_Map_Play_pong.Service.Repositories;

namespace ping_Map_Play_pong.Controllers;

[ApiController]
[Route("[controller]")]

public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserRepository _userRepository;

    public UserController(ILogger<UserController> logger, IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    [HttpGet("users")]
    public ActionResult<IEnumerable<User>> Get()
    {
        return Ok(_userRepository.GetAll());
    }
}