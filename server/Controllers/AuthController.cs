using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ping_Map_Play_pong.Contracts;
using ping_Map_Play_pong.Model;
using ping_Map_Play_pong.Service.Authentication;
using ping_Map_Play_pong.Service.Repositories;

namespace ping_Map_Play_pong.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authenticationService;
    private readonly IConfiguration _configuration;

    public AuthController(IAuthService authenticationService, IConfiguration configuration, IUserRepository userRepository)
    {
        _authenticationService = authenticationService;
        _configuration = configuration;
        _userRepository = userRepository;
    }

    [HttpPost("Register")]
    public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authenticationService.RegisterAsync(request.Email, request.Username, request.Password, (_configuration.GetSection("AppRoles:UserRoleName").Value));

        if (!result.Success)
        {
            AddErrors(result);
            return BadRequest(ModelState);
        }

        return CreatedAtAction(nameof(Register), new RegistrationResponse(result.Email, result.UserName));
    }
    
    


    private void AddErrors(AuthResult result)
    {
        foreach (var error in result.ErrorMessages)
        {
            ModelState.AddModelError(error.Key, error.Value);
        }
    }
    
    
    [HttpPost("Login")]
    public async Task<ActionResult<User>> Authenticate([FromBody] AuthRequest request)
    {
        Console.WriteLine($"Received AuthRequest: Email={request.Email}, Password={request.Password}");
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authenticationService.LoginAsync(request.Email, request.Password);

        if (!result.Success)
        {
            AddErrors(result);
            return BadRequest(ModelState);
        }

        var user = _userRepository.GetByEmail(result.Email);

        // Set the cookie here
        HttpContext.Response.Cookies.Append("access_token", result.Token, new CookieOptions { HttpOnly = true, Expires = DateTime.Now.AddMinutes(30)});
      
        return Ok(user);
    }
    
    
    
    [HttpPost("Logout")]
    public IActionResult Logout()
    {
        HttpContext.Response.Cookies.Delete("access_token");
        return Ok();
    }
}