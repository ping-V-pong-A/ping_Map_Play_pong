using ping_Map_Play_pong.Model;

namespace ping_Map_Play_pong.Service.Repositories;

public interface IUserRepository
{
    IEnumerable<User> GetAll();
    User GetById(int userId);
    User GetByEmail(string email);
    void Add(User user);
    void Update(User user);
}