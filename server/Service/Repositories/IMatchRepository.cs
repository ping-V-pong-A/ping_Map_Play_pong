using ping_Map_Play_pong.Data;
using ping_Map_Play_pong.Model.DataModels;

namespace ping_Map_Play_pong.Service.Repositories;

public interface IMatchRepository
{
    IEnumerable<Match> GetByTableId(int tableId);
    IEnumerable<Match> GetAll();
    IEnumerable<Match> GetByUserId(int userId);
    IEnumerable<Match> GetByPlayer1IdAndPlayer2Id(int player1Id, int player2Id);
    IEnumerable<Match> GetByDate(DateTime date);
    Match GetById(int matchId);
    void Add(Match match);
    void Delete(Match match);
    void Update(Match match);
}