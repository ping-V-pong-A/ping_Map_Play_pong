using ping_Map_Play_pong.Data;
using ping_Map_Play_pong.Model;

namespace ping_Map_Play_pong.Service.Repositories;

public class UserRepository : IUserRepository
{
    private pingMapPlayPongContext _dbContext;

    public UserRepository(pingMapPlayPongContext context)
    {
        _dbContext = context;
    }
    
    public IEnumerable<User> GetAll()
    {
        return _dbContext.Users.ToList();
    }

    public User GetById(int id)
    {
        return _dbContext.Users.FirstOrDefault(u => u.Id == id);
    }

    public User GetByName(string userName)
    {
        return _dbContext.Users.FirstOrDefault(u => u.Name == userName);
    }

    public void Add(User user)
    {
        _dbContext.Add(user);
        _dbContext.SaveChanges();
    }

    public void Update(User user)
    {
        _dbContext.Update(user);
        _dbContext.SaveChanges();
    }
}