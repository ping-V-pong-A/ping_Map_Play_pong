using ping_Map_Play_pong.Model.DataModels;

namespace ping_Map_Play_pong.Service.Repositories;

public interface IPairMatchRepository
{
    IEnumerable<PairMatch> GetAll();
    IEnumerable<PairMatch> GetByDate(DateTime date);
    PairMatch GetById(int matchId);
    void Add(PairMatch match);
    void Delete(PairMatch match);
    void Update(PairMatch match);
}