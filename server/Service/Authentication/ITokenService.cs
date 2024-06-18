using Microsoft.AspNetCore.Identity;


namespace ping_Map_Play_pong.Service.Authentication;

public interface ITokenService
{
    public string CreateToken(IdentityUser user);
}