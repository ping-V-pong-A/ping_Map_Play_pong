using ping_Map_Play_pong.Model.DataModels;

namespace ping_Map_Play_pong.Service.Repositories;

public interface IPairMachRepository
{
    IEnumerable<PairMatch> GetByTableId(int tableId);
    IEnumerable<PairMatch> GetByUserId(int userId);
    IEnumerable<PairMatch> GetByDate(DateTime date);
    PairMatch GetById(int matchId);
    void Add(PairMatch match);
    void Delete(PairMatch match);
    void Update(PairMatch match);
}