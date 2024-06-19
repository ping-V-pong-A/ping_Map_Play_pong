using Microsoft.AspNetCore.Identity;


namespace ping_Map_Play_pong.Service.Authentication;

public interface ITokenService
{
    string CreateToken(IdentityUser user, string role);
}