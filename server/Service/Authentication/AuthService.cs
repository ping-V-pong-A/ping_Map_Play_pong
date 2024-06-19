using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using ping_Map_Play_pong.Model;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.Enums;
using ping_Map_Play_pong.Service.Repositories;

namespace ping_Map_Play_pong.Service.Authentication;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;

    public AuthService(UserManager<IdentityUser> userManager, ITokenService tokenService, IUserRepository userRepository)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _userRepository = userRepository;
    }

    public async Task<AuthResult> RegisterAsync(string email, string username, string password, string role)
    {
        var user = new IdentityUser { UserName = username, Email = email };
       
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return FailedRegistration(result, email, username);
        }

        
        await _userManager.AddToRoleAsync(user, role);
        var userEmail = user.Email;
        var newUser = new User
        {
            RegistrationDate = DateTime.Now,
            CheckedInTables = new List<Table>(),
            IdentityUserEmail = userEmail,
            Rank = Rank.Beginner,
        };
     

        _userRepository.Add(newUser);
        return new AuthResult(true, email, username, "");
    }

    
    
    private static AuthResult FailedRegistration(IdentityResult result, string email, string username)
    {
        var authResult = new AuthResult(false, email, username, "");

        foreach (var error in result.Errors)
        {
            authResult.ErrorMessages.Add(error.Code, error.Description);
        }

        return authResult;
    }
    
    
    public async Task<AuthResult> LoginAsync(string email, string password)
    {
        var managedUser = await _userManager.FindByEmailAsync(email);

        if (managedUser == null)
        {
            return InvalidEmail(email);
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, password);
        if (!isPasswordValid)
        {
            return InvalidPassword(email, managedUser.UserName);
        }

        var roles = await _userManager.GetRolesAsync(managedUser);
        var accessToken = _tokenService.CreateToken(managedUser, roles[0]);
      
      
        return  new AuthResult(true, managedUser.Email, managedUser.UserName, accessToken);
        

    }

    private static AuthResult InvalidEmail(string email)
    {
        var result = new AuthResult(false, email, "", "");
        result.ErrorMessages.Add("Bad credentials", "Invalid email");
        return result;
    }
    
    


    private static AuthResult InvalidPassword(string email, string userName)
    {
        var result = new AuthResult(false, email, userName, "");
        result.ErrorMessages.Add("Bad credentials", "Invalid password");
        return result;
    }
}