using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ping_Map_Play_pong.Model.DataModels;

public class PairMatch
{
    [Key]
    public int Id { get; set; }
    
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Table Table { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    [ForeignKey("Team1Id")]
    public Team Team1 { get; set; }
    public int Team1Point { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    [ForeignKey("Team2Id")]
    public Team Team2 { get; set; }
    public int Team2Point { get; set; }
    
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