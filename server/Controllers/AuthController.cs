using Microsoft.AspNetCore.Mvc;
using ping_Map_Play_pong.Contracts;
using ping_Map_Play_pong.Service.Authentication;
using ping_Map_Play_pong.Service.Repositories;

namespace ping_Map_Play_pong.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authenticationService;
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthController(IAuthService authenticationService, IConfiguration configuration, IUserRepository userRepository)
    {
        _authenticationService = authenticationService;
        _configuration = configuration;
        _userRepository = userRepository;
    }

    [HttpPost("sign-up")]
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
    
    [HttpPost("sign-in")]
    public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
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
        
        HttpContext.Response.Cookies.Append("access_token", result.Token, new CookieOptions { HttpOnly = true, Expires = DateTime.Now.AddMinutes(30)});
      
        return Ok(user);
    }
    
    [HttpPost("sign-out")]
    public IActionResult Logout()
    {
        HttpContext.Response.Cookies.Delete("access_token");
        return Ok();
    }

    private void AddErrors(AuthResult result)
    {
        foreach (var error in result.ErrorMessages)
        {
            ModelState.AddModelError(error.Key, error.Value);
        }
    }
}
