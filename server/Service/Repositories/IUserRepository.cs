using ping_Map_Play_pong.Model;

namespace ping_Map_Play_pong.Service.Repositories;

public interface IUserRepository
{
    IEnumerable<User> GetAll();
    User GetById(int userId);
    User GetByName(string userName);
    void Add(User user);
    void Update(User user);

    void Delete(User user);
}