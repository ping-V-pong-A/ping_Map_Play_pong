using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ping_Map_Play_pong.Model;

namespace ping_Map_Play_pong.Service;

public interface IUserService
{
    IEnumerable<User> GetAll();
    User GetById(int userId);
    void Update();
    void Delete(IdentityUser user);
}