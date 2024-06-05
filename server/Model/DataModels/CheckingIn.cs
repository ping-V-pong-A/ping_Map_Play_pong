using System.ComponentModel.DataAnnotations;

namespace ping_Map_Play_pong.Model.DataModels;

public class CheckingIn
{
    [Key]
    public int Id { get; init; }
    
    public User User { get; init; }
    public Table Table { get; init; }
    
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    
}