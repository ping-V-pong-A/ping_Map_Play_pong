using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ping_Map_Play_pong.Model.DataModels;

public class CheckingIn
{
    [Key]
    public int Id { get; init; }
    
    [ForeignKey("UserId")]
    public int UserId { get; set; }
    
    [ForeignKey("TableId")]
    public int TableId { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
}