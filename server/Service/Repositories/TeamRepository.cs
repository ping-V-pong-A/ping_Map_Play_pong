using ping_Map_Play_pong.Data;
using ping_Map_Play_pong.Model.DataModels;

namespace ping_Map_Play_pong.Service.Repositories;

public class TeamRepository : ITeamRepository
{
    private PingMapPlayPongContext _dbContext;

    public TeamRepository(PingMapPlayPongContext context)
    {
        _dbContext = context;
    }

    public Team GetById(int teamId)
    {
        return _dbContext.Teams.FirstOrDefault(t => t.Id == teamId);
    }
    public IEnumerable<Team> GetAll()
    {
        return _dbContext.Teams.ToList();
    }

    public IEnumerable<Team> GetByUserId(int userId)
    {
        return _dbContext.Teams.Where(t => t.Player1.Id == userId || t.Player2.Id == userId);
    }


    public IEnumerable<Team> GetByPlayersId(int player1Id, int player2Id)
    {
        return _dbContext.Teams.Where(t => 
            (t.Player1.Id == player1Id || t.Player2.Id == player1Id) && 
            (t.Player1.Id == player2Id || t.Player2.Id == player2Id));
    }



    public void Add(Team team)
    {
        _dbContext.Add(team);
        _dbContext.SaveChanges();
    }

    public void Delete(Team team)
    {
        _dbContext.Remove(team);
        _dbContext.SaveChanges();
    }

    public void Update(Team team)
    {
        _dbContext.Update(team);
        _dbContext.SaveChanges();
    }
}