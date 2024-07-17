using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ping_Map_Play_pong.Data;
using ping_Map_Play_pong.Model;

namespace ping_Map_Play_pong.Service.Repositories;

public class UserRepository : IUserRepository
{
    private PingMapPlayPongContext _dbContext;

    public UserRepository(PingMapPlayPongContext context)
    {
        _dbContext = context;
    }
    
    public IEnumerable<User> GetAll()
    {
        return _dbContext.Users
            .Include(user => user.CheckedInTables)
            .ToList();
    }

    public User GetById(int userId)
    {
        return _dbContext.Users.FirstOrDefault(u => u.Id == userId);
    }

    public User GetByEmail(string email)
    {
        return _dbContext.Users.FirstOrDefault(u => u.IdentityUserEmail == email);
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