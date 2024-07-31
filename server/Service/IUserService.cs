using ping_Map_Play_pong.Model.RequestModels;
using ping_Map_Play_pong.Model.ResponseModels;

namespace ping_Map_Play_pong.Service;

public interface IUserService
{
    IEnumerable<UserResponse> GetAll();
    UserResponse GetById(int userId);
    UserResponse GetByEmail(string userEmail);
    void Update(int userId, UserRequest request);
    void Delete(int userId);
}