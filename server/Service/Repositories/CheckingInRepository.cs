using ping_Map_Play_pong.Data;
using ping_Map_Play_pong.Model.DataModels;

namespace ping_Map_Play_pong.Service.Repositories;

public class CheckingInRepository : ICheckingInRepository
{
    private PingMapPlayPongContext _dbContext;
    
    public CheckingInRepository(PingMapPlayPongContext context)
    {
        _dbContext = context;
    }

    public IEnumerable<CheckingIn> GetAll()
    {
        return _dbContext.CheckingIns.ToList();
    }
    
    public CheckingIn GetById(int checkingInId)
    {
        return _dbContext.CheckingIns.FirstOrDefault(c => c.Id == checkingInId);
    }

    public IEnumerable<CheckingIn> GetByUserId(int userId)
    {
        return _dbContext.CheckingIns.Where(c => c.UserId == userId);
    }

    public void Add(CheckingIn checkingIn)
    {
        _dbContext.Add(checkingIn);
        _dbContext.SaveChanges();
    }

    public void Delete(CheckingIn checkingIn)
    {
        _dbContext.Remove(checkingIn);
        _dbContext.SaveChanges();
    }

    public void Update(CheckingIn checkingIn)
    {
        _dbContext.Update(checkingIn);
        _dbContext.SaveChanges();
    }
}