using ping_Map_Play_pong.Model;

namespace ping_Map_Play_pong.Service.Repositories;

public class UserRepository : IUserRepository
{
    private readonly List<User> _users = [];
    
    public IEnumerable<User> GetAll()
    {
        return _users;
    }

    public User GetById(int id)
    {
        throw new NotImplementedException();
    }

    public User GetByName(string userName)
    {
        throw new NotImplementedException();
    }

    public void Add(string username, string email)
    {
        throw new NotImplementedException();
    }

    public void Update(int id)
    {
        throw new NotImplementedException();
    }
}