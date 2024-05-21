using ping_Map_Play_pong.Model;

namespace ping_Map_Play_pong.Service.Repositories;

public interface IUserRepository
{
    IEnumerable<User> GetAll();
    User GetById(int id);
    void Register(string username, string email);
    void Edit(int id);
    
}