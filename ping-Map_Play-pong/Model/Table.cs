namespace ping_Map_Play_pong.Model;

public class Table
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Coordinate Coordinate { get; set; }

    public List<CheckingIn> CheckingIns { get; }
    public List<Match>LeaderBoard { get; }

    public Table()
    {
        CheckingIns = new List<CheckingIn>();
        LeaderBoard = new List<Match>();
    }
}