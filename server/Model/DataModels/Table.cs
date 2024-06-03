using ping_Map_Play_pong.Model.Matches;

namespace ping_Map_Play_pong.Model.DataModels;

public class Table
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Coordinate Coordinate { get; set; }

    public List<CheckingIn> CheckingIns { get; }
    public List<MatchBase>LeaderBoard { get; }

    public Table()
    {
        CheckingIns = new List<CheckingIn>();
        LeaderBoard = new List<MatchBase>();
    }
}