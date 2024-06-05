using System.ComponentModel.DataAnnotations;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.Enums;

namespace ping_Map_Play_pong.Model;

public class User
{
    [Key]
    public int Id { get; init; }
    public string Name { get; init; }
    [Required]
    public string Email { get; init; }
    public string Password { get; init; }
    public DateTime RegistrationDate { get; init; }
    
    public ICollection<Table> CheckedInTables { get; init; }
    
    // TODO 
    // add favorite tables
    
    public Rank Rank { get; init; }
}