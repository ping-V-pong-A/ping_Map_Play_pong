using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ping_Map_Play_pong.Model.DataModels;

public class Team
{
    [Key]
    public int Id { get; set; }
    
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public User Player1 { get; set; }
    
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public User Player2 { get; set; }
}