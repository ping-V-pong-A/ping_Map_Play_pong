using Microsoft.AspNetCore.Identity;
using ping_Map_Play_pong.Model;
using ping_Map_Play_pong.Service.Repositories;

namespace ping_Map_Play_pong.Service;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IUserRepository _userRepository;
    private readonly UserManager<IdentityUser> _userManager;

    public UserService(ILogger<UserService> logger, IUserRepository userRepository, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _userRepository = userRepository;
        _userManager = userManager;
    }

    public IEnumerable<User> GetAll() => _userRepository.GetAll();

    public User GetById(int userId) => _userRepository.GetById(userId);

    public void Update()
    {
        throw new NotImplementedException();
    }

    public void Delete(IdentityUser user)
    {

        
        _userManager.DeleteAsync(user);
    }

}