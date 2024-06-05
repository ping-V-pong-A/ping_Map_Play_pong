using ping_Map_Play_pong.Model.DataModels;

namespace ping_Map_Play_pong.Service.Repositories;

public interface ITeamRepository
{
    IEnumerable<Team> GetAll();

    Team GetById(int teamId);
    IEnumerable<Team> GetByUserId(int userId);
    IEnumerable<Team> GetByPlayersId(int player1Id, int player2Id);
    void Add(Team team);
    void Delete(Team team);
    void Update(Team team);
}