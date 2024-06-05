using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ping_Map_Play_pong.Model.DataModels;

public class PairMatch
{
    [Key]
    public int Id { get; init; }
    
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Table Table { get; init; }
    
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    [ForeignKey("Team1Id")]
    public Team Team1 { get; init; }
    public int Team1Point { get; init; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    [ForeignKey("Team2Id")]
    public Team Team2 { get; init; }
    public int Team2Point { get; init; }
    
    public Team Winner
    {
        get
        {
            if (Team1Point > Team2Point)
            {
                return Team1;
            }
            return Team2;
            
        }
    }
}