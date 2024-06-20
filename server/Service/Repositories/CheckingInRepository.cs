using ping_Map_Play_pong.Data;
using ping_Map_Play_pong.Model.DataModels;

namespace ping_Map_Play_pong.Service.Repositories;

public class CheckingInRepository : ICheckingInRepository
{
    private PingMapPlayPongContext _dbContext;

    public CheckingIn GetById(int checkingInId)
    {
        return _dbContext.CheckingIns.FirstOrDefault(c => c.Id == checkingInId);
    }
    
    public CheckingInRepository(PingMapPlayPongContext context)
    {
        _dbContext = context;
    }

    public IEnumerable<CheckingIn> GetAll()
    {
        return _dbContext.CheckingIns.ToList();
    }

    public IEnumerable<CheckingIn> GetByTableId(int tableId)
    {
        return _dbContext.CheckingIns.Where(c => c.TableId == tableId);
    }

    public IEnumerable<CheckingIn> GetByUserId(int userId)
    {
        return _dbContext.CheckingIns.Where(c => c.UserId == userId);
    }

    public IEnumerable<CheckingIn> GetByUserIdAndDate(int userId, DateTime date)
    {
        return _dbContext.CheckingIns.Where(c => c.UserId == userId && c.StartDate.Date == date);
    }


    
    public CheckingIn GetByUserIdAndStartDateTime(int userId, DateTime startDateTime)
    {
        return _dbContext.CheckingIns.FirstOrDefault(c => c.UserId == userId &&
                                                          c.StartDate.Year == startDateTime.Year &&
                                                          c.StartDate.Month == startDateTime.Month &&
                                                          c.StartDate.Day == startDateTime.Day &&
                                                          c.StartDate.Hour == startDateTime.Hour &&
                                                          c.StartDate.Minute == startDateTime.Minute);
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