using Microsoft.AspNetCore.Identity;

namespace ping_Map_Play_pong.Service.Authentication;

public class AuthenticationSeeder
{

    private RoleManager<IdentityRole> roleManager;
    private readonly IConfiguration _configuration;

    public AuthenticationSeeder(RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        this.roleManager = roleManager;
        _configuration = configuration;
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