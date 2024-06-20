using ping_Map_Play_pong.Model.DataModels;

namespace ping_Map_Play_pong.Model.ResponseModels;

public class TableResponse
{
    public int Id { get; init; }
    public string Name { get; init; }
    public double Lat { get; init; }
    public double Lon { get; init; }
    public List<CheckingIn> CheckingIns { get; init; } = [];
    public List<Match> Matches { get; init; } = [];
    public List<PairMatch> PairMatches { get; init; } = [];
}
