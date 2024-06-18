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
    
    public void AddAdmin()
    {
        var tAdmin = CreateAdminIfNotExists();
        tAdmin.Wait();
    }

    private async Task CreateAdminIfNotExists()
    {var email =  _configuration.GetSection("AdminSettings:Email").Value;
        var username = _configuration.GetSection("AdminSettings:Username").Value;
        var password = _configuration.GetSection("AdminSettings:Password").Value;
        var adminInDb = await userManager.FindByEmailAsync(email);
        if (adminInDb == null)
        {
            var admin = new IdentityUser { UserName = username, Email = email };
            var adminCreated = await userManager.CreateAsync(admin, password);

            if (adminCreated.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, _configuration.GetSection("AppRoles:AdminRoleName").Value);
            }
        }
    }

}