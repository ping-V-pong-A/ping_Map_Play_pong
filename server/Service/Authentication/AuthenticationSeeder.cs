using Microsoft.AspNetCore.Identity;

namespace ping_Map_Play_pong.Service.Authentication;

public class AuthenticationSeeder
{

    private RoleManager<IdentityRole> roleManager;
    private readonly IConfiguration _configuration;
    private UserManager<IdentityUser> userManager;

    
    
    
    public AuthenticationSeeder(RoleManager<IdentityRole> roleManager, IConfiguration configuration, UserManager<IdentityUser> userManager)
    {
        this.roleManager = roleManager;
        _configuration = configuration;
        this.userManager = userManager;
    }
    

    
    public void AddRoles()
    {
        var tAdmin = CreateAdminRole(roleManager);
        tAdmin.Wait();

        var tUser = CreateUserRole(roleManager);
        tUser.Wait();
    }

    private async Task CreateAdminRole(RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole(_configuration.GetSection("AppRoles:AdminRoleName").Value));
    }

    async Task CreateUserRole(RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole(_configuration.GetSection("AppRoles:UserRoleName").Value));
    }

}