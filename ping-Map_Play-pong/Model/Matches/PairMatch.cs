namespace ping_Map_Play_pong.Model.Matches;

public class PairMatch :MatchBase
{
    public User[] Team1 { get; }
    public User[] Team2 { get; }
    public int Team1Point { get; }
    public int Team2Point { get; }
    


    public override IEnumerable<User> GetWinner()
    {
        if (Team1Point > Team2Point)
        {
           return Team1;
        }
        else
        {
            return Team2;
        }
    }
}