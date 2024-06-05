using ping_Map_Play_pong.Model.DataModels;

namespace ping_Map_Play_pong.Service.Repositories;

public interface ICheckingInRepository
{
    // C.R.U.D.
    
    IEnumerable<CheckingIn> GetAll();

    CheckingIn GetById(int checkingInId);
    IEnumerable<CheckingIn> GetByTableId(int tableId);
    IEnumerable<CheckingIn> GetByUserId(int userId);
    IEnumerable<CheckingIn> GetByUserIdAndDate(int userId, DateTime date);
    CheckingIn GetByUserIdAndStartDateTime(int userId, DateTime startDateTime);
    void Add(CheckingIn checkingIn);
    void Delete(CheckingIn checkingInId);
    void Update(CheckingIn checkingIn);
    
    
}