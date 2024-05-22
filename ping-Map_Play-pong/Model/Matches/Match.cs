using System.Runtime.InteropServices.JavaScript;

namespace ping_Map_Play_pong.Model;

public class Match
{
    public User Player1 { get; set; }
    public int Player1Point { get; set; }
    public User Player2 { get; set; }
    public int Player2Point { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public User Winner { get; set; }
    public Table Table { get; set; }
   

    public Match(User player1, DateTime startDate, DateTime endDate, Table table)
    {
        Player1 = player1;
        StartDate = startDate;
        EndDate = endDate;
        Table = table;
    }

    public User GetWinner()
    {
        if (Player1Point > Player2Point)
        {
            return Player1;
        }
        else
        {
            return Player2;
        }
    }
    
} 