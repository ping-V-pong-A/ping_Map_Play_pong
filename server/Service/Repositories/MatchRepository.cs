using ping_Map_Play_pong.Data;
using ping_Map_Play_pong.Model.DataModels;


namespace ping_Map_Play_pong.Service.Repositories;


public class MatchRepository : IMatchRepository
{
    private PingMapPlayPongContext _dbContext;

    public MatchRepository(PingMapPlayPongContext context)
    {
        _dbContext = context;
    }

    public IEnumerable<Match> GetAll()
    {
        return _dbContext.Matches.ToList();
    }
    
    public IEnumerable<Match> GetByTableId(int tableId)
    {
        return _dbContext.Matches.Where(m => m.Table.Id == tableId);
    }


    public IEnumerable<Match> GetByUserId(int userId)
    {
        return _dbContext.Matches.Where(m => m.Player1.Id == userId || m.Player2.Id == userId);
    }


    public IEnumerable<Match> GetByPlayer1IdAndPlayer2Id(int player1Id, int player2Id)
    {
        return _dbContext.Matches.Where(m => (m.Player1.Id == player1Id || m.Player2.Id == player1Id) &&
                                             (m.Player1.Id == player2Id || m.Player2.Id == player2Id));
    }


    public IEnumerable<Match> GetByDate(DateTime date)
    {
        return _dbContext.Matches.Where(m => m.StartDate.Date == date);
    }

    
    public Match GetById(int matchId)
    {
        return _dbContext.Matches.First(m => m.Id == matchId);
    }


    public void Add(Match match)
    {
        _dbContext.Add(match);
        _dbContext.SaveChanges();
    }


    public void Delete(Match match)
    {
        _dbContext.Remove(match);
        _dbContext.SaveChanges();
    }


    public void Update(Match match)
    {
        _dbContext.Update(match);
        _dbContext.SaveChanges();
    }
}