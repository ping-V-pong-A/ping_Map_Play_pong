using ping_Map_Play_pong.Data;
using ping_Map_Play_pong.Model.DataModels;

namespace ping_Map_Play_pong.Service.Repositories;

public class CheckingInRepository : ICheckingInRepository
{
    private pingMapPlayPongContext _dbContext;
    
    public CheckingInRepository(pingMapPlayPongContext context)
    {
        _dbContext = context;
    }

    public IEnumerable<CheckingIn> GetAll()
    {
        return _dbContext.CheckingIns.ToList();
    }

    public IEnumerable<CheckingIn> GetByTableId(int tableId)
    {
        return _dbContext.CheckingIns.Where(c => c.Table.Id == tableId);
    }

    public IEnumerable<CheckingIn> GetByUserId(int userId)
    {
        return _dbContext.CheckingIns.Where(c => c.User.Id == userId);
    }

    public IEnumerable<CheckingIn> GetByUserIdAndDate(int userId, DateTime date)
    {
        return _dbContext.CheckingIns.Where(c => c.User.Id == userId && c.StartDate.Date == date);
    }
    
  

    public CheckingIn GetByUserIdAndStartDateTime(int userId, DateTime startDateTime)
    {
        return _dbContext.CheckingIns.FirstOrDefault(c => c.User.Id == userId && c.StartDate == startDateTime);
    }
    
 


    public void Add(CheckingIn checkingIn)
    {
        _dbContext.Add(checkingIn);
        _dbContext.SaveChanges();
    }


    public void Delete(CheckingIn checkingInId)
    {
        _dbContext.Remove(checkingInId);
        _dbContext.SaveChanges();
    }


    public void Update(CheckingIn checkingIn)
    {
        _dbContext.Update(checkingIn);
        _dbContext.SaveChanges();
    }
}