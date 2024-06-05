using ping_Map_Play_pong.Data;
using ping_Map_Play_pong.Model.DataModels;

namespace ping_Map_Play_pong.Service.Repositories;

public class PairMatchRepository : IPairMatchRepository
{
    private pingMapPlayPongContext _dbContext;

    public PairMatchRepository(pingMapPlayPongContext context)
    {
        _dbContext = context;
    }

    public IEnumerable<PairMatch> GetByTableId(int tableId)
    {
        return _dbContext.PairMatches.Where(pm => pm.Table.Id == tableId);
    }


    public IEnumerable<PairMatch> GetAll()
    {
        return _dbContext.PairMatches.ToList();
    }
    public IEnumerable<PairMatch> GetByUserId(int userId)
    {
        return _dbContext.PairMatches.Where(pm => 
            (pm.Team1.Player1.Id == userId || 
             pm.Team1.Player2.Id == userId || 
             pm.Team2.Player1.Id == userId || 
             pm.Team2.Player2.Id == userId));
    }


    public IEnumerable<PairMatch> GetByDate(DateTime date)
    {
        return _dbContext.PairMatches.Where(pm => pm.StartDate.Date == date);
    }


    public PairMatch GetById(int matchId)
    {
        return _dbContext.PairMatches.FirstOrDefault(pm => pm.Id == matchId);
    }


    public void Add(PairMatch match)
    {
        _dbContext.Add(match);
        _dbContext.SaveChanges();
    }


    public void Delete(PairMatch match)
    {
        _dbContext.Remove(match);
        _dbContext.SaveChanges();
    }


    public void Update(PairMatch match)
    {
        _dbContext.Update(match);
        _dbContext.SaveChanges();
    }
}