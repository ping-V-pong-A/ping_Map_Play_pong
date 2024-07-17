using ping_Map_Play_pong.Model.DataModels;

namespace ping_Map_Play_pong.Service.Repositories;

public interface ICheckingInRepository
{
    IEnumerable<CheckingIn> GetAll();
    CheckingIn GetById(int checkingInId);
    IEnumerable<CheckingIn> GetByUserId(int userId);
    void Add(CheckingIn checkingIn);
    void Delete(CheckingIn checkingInId);
    void Update(CheckingIn checkingIn);
}