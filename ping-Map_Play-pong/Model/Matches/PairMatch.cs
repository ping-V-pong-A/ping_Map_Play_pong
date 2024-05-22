namespace ping_Map_Play_pong.Model.Matches;

public class PairMatch : Match
{
    public PairMatch(User player1, DateTime startDate, DateTime endDate, Table table) : base(player1, startDate, endDate, table)
    {
    }
}