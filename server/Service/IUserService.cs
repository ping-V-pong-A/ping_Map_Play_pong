using Microsoft.AspNetCore.Identity;
using ping_Map_Play_pong.Model;
using ping_Map_Play_pong.Model.RequestModels;

namespace ping_Map_Play_pong.Service;

public interface IUserService
{
    IEnumerable<User> GetAll();
    User GetById(int userId);
    void Update(UserRequest request);
    void Delete(User user);
}