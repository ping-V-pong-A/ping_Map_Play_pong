using System.Runtime.InteropServices.JavaScript;
using ping_Map_Play_pong.Model.Matches;

namespace ping_Map_Play_pong.Model;

public class Match : MatchBase
{
    public User Player1 { get; set; }
    public int Player1Point { get; set; }
    public User Player2 { get; set; }
    public int Player2Point { get; set; }
    public override IEnumerable<User> GetWinner()
    {
        if (Player1Point > Player2Point)
        {
          yield return Player1;
        }
        else
        {
          yield return Player2;
        }
    }
    
} 