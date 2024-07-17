using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.RequestModels;

namespace ping_Map_Play_pong.Service;

public interface IMatchService
{
    IEnumerable<Match> GetAll();
    Match GetById(int matchId);
    IEnumerable<Match> GetByUserId(int userId);
    IEnumerable<Match> GetByPlayersId(int player1Id, int player2Id);
    IEnumerable<Match> GetByDate(DateTime date);
    void PostToDb(MatchRequest request);
    void Update(Match match);
    void DeleteFromDb(Match match);
}