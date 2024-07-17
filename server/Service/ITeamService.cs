using ping_Map_Play_pong.Model.DataModels;

namespace ping_Map_Play_pong.Service;

public interface ITeamService
{
    IEnumerable<Team> GetAll();
    IEnumerable<Team> GetByUserId(int userId);
    Team GetByPlayersId(int player1Id, int player2Id);
    void PostToDb(int player1Id, int player2Id);
    void DeleteFromDb(int teamId);
}