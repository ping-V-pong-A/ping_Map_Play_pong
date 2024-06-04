using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ping_Map_Play_pong.Model.DataModels;

public class Match
{
    [Key]
    public int Id { get; set; }
    
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Table Table { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    [ForeignKey("Player1Id")]
    public User Player1 { get; set; }
    public int Player1Point { get; set; }
    
    [DeleteBehavior(DeleteBehavior.NoAction)]
    [ForeignKey("Player2Id")]
    public User Player2 { get; set; }
    public int Player2Point { get; set; }

    public User Winner
    {
        get
        {
            if (Player1Point > Player2Point)
            {
                return Player1;
            }
            return Player2;
            
        }
    }
    
} 