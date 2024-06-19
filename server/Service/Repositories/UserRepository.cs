using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ping_Map_Play_pong.Data;
using ping_Map_Play_pong.Model;

namespace ping_Map_Play_pong.Service.Repositories;

public class UserRepository : IUserRepository
{
    private PingMapPlayPongContext _dbContext;
    private readonly UserManager<IdentityUser> _userManager;

    public UserRepository(PingMapPlayPongContext context,UserManager<IdentityUser> userManager)
    {
        _dbContext = context;
        _userManager = userManager;
    }
    
    public IEnumerable<User> GetAll()
    {
        return _dbContext.Users.ToList();
    }

    public User GetById(int userId)
    {
        return _dbContext.Users.FirstOrDefault(u => u.Id == userId);
    }


    public User GetByEmail(string email)
    {
        return _dbContext.Users.FirstOrDefault(u => u.IdentityUserEmail == email);
    }
   
    /*public async Task<User?> GetByNameAsync(string userName)
    {
        var identityUser = await _userManager.FindByNameAsync(userName);

        if (identityUser == null)
        {
            return null;
        }

        Console.WriteLine($"identityusers username: {identityUser.UserName}");
        Console.WriteLine($"identityusers email: {identityUser.Email}");
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.IdentityUserEmail == identityUser.Email);
      
        return user;
    }*/
    
    public async Task<User?> GetByNameAsync(string userName)
    {
        var identityUser = await _userManager.FindByNameAsync(userName);

        if (identityUser == null)
        {
            return null;
        }

        var user = await _dbContext.Users
            .Where(u => u.IdentityUserEmail == identityUser.Email)
            .Select(u => new User {
                Id = u.Id,
                RegistrationDate = u.RegistrationDate,
                CheckedInTables = u.CheckedInTables,
                Rank = u.Rank
            })
            .FirstOrDefaultAsync();

        return user;
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

    public void Delete(User user)
    {
        _dbContext.Remove(user);
        _dbContext.SaveChanges();
    }
}