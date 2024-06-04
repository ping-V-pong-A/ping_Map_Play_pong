using System.ComponentModel.DataAnnotations;

namespace ping_Map_Play_pong.Model.DataModels;

public class CheckingIn
{
    [Key]
    public int Id { get; set; }
    
    public User User { get; }
    public Table Table { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
}