using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ping_Map_Play_pong.Model.DataModels;

public class CheckingIn
{
    [Key]
    public int Id { get; init; }
    
    [ForeignKey("UserId")]
    public User User { get; init; }
    
    [ForeignKey("TableId")]
    public Table Table { get; init; }
    
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    
}