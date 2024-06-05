using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ping_Map_Play_pong.Model.DataModels;

public class Match
{
    [Key]
    public int Id { get; init; }
    
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public Table Table { get; init; }
    
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }

    [DeleteBehavior(DeleteBehavior.NoAction)]
    [ForeignKey("Player1Id")]
    public User Player1 { get; init; }
    public int Player1Point { get; init; }
    
    [DeleteBehavior(DeleteBehavior.NoAction)]
    [ForeignKey("Player2Id")]
    public User Player2 { get; init; }
    public int Player2Point { get; init; }

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