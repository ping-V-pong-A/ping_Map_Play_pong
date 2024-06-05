using ping_Map_Play_pong.Model;

namespace ping_Map_Play_pong.Service.Repositories;

public interface IUserRepository
{
    IEnumerable<User> GetAll();
    User GetById(int id);
    User GetByName(string userName);
    void Add(string userName, string email);
    void Update(int userId);
}