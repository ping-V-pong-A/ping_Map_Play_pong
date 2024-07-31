using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.RequestModels;

namespace ping_Map_Play_pong.Service;

public interface ICheckingInService
{
    IEnumerable<CheckingIn> GetAll();
    CheckingIn GetById(int checkingInId);
    IEnumerable<CheckingIn> GetByUserId(int userId);
    void PostToDb(CheckInRequest request);
    void Update(int checkingInId, CheckInRequest request);
    void DeleteFromDb(int checkingInId);
}