using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ping_Map_Play_pong.Model.DataModels;

public class Table
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Coordinate Coordinate { get; set; }
    
    public ICollection<CheckingIn> CheckingIns { get; set; }
    public ICollection<Match> LeaderBoard { get; set; }
    public ICollection<PairMatch> PairMatchesLeaderBoard { get; set; }
}