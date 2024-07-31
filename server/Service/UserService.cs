using Microsoft.AspNetCore.Identity;
using ping_Map_Play_pong.Model.Exceptions;
using ping_Map_Play_pong.Model.RequestModels;
using ping_Map_Play_pong.Model.ResponseModels;
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

    public IEnumerable<UserResponse> GetAll()
    {
        var users = _userRepository.GetAll();
            
        var respUsers = users.Select(user => new UserResponse
        {
            Id = user.Id,
            RegistrationDate = user.RegistrationDate,
            CheckedInTables = user.CheckedInTables.ToList(),
            Rank = user.Rank
        }).ToList();


        return respUsers;
    }

    public UserResponse GetById(int userId)
    {
        var user = _userRepository.GetById(userId);

        var respUser = new UserResponse()
        {
            UserName = user.IdentityUser.UserName
        };

        return respUser;
    }

    public UserResponse GetByEmail(string userEmail)
    {
        var user = _userRepository.GetByEmail(userEmail);

        var respUser = new UserResponse()
        {
            Id = user.Id,
            UserName = user.IdentityUser.UserName,
            Rank = user.Rank,
            RegistrationDate = user.RegistrationDate
        };

        return respUser;
    }

    public void Update(int userId, UserRequest request)
    {
        var user = _userRepository.GetById(userId);
            
        if (user == null)
        {
            throw new NotFoundException($"user not found with Id:{userId}");
        }

        user.Rank = request.Rank;
        
        _userRepository.Update(user);
    }

    public async void Delete(int userId)
    {
        var user = _userRepository.GetById(userId);

        if (user == null)
        {
            throw new NotFoundException($"user not found with Id:{userId}");
        }

        var identityUser = await _userManager.FindByEmailAsync(user.IdentityUserEmail);
        await _userManager.DeleteAsync(identityUser);
    }

}