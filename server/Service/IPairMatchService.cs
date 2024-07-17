using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.RequestModels;

namespace ping_Map_Play_pong.Service;

public interface IPairMatchService
{
    IEnumerable<PairMatch> GetAll();
    PairMatch GetById(int pairMatchId);
    IEnumerable<PairMatch> GetByDate(DateTime date);
    void PostToDb(PairMatchRequest request);
    void Update(PairMatch pairMatch);
    void DeleteFromDb(PairMatch pairMatch);
}