namespace ping_Map_Play_pong.Model.Matches;

public abstract class MatchBase
{
  
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Table Table { get; set; }

    public  abstract IEnumerable<User> GetWinner();
}