using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.Enums;

namespace ping_Map_Play_pong.Model;

public class User
{
    [Key]
    public int Id { get; init; }
    
    public DateTime RegistrationDate { get; init; }
    
    public ICollection<Table> CheckedInTables { get; init; }
    

    
    [ForeignKey("IdentityUserEmail")]
    public string IdentityUserEmail { get; set; }

    // Navigation property to IdentityUser
    public IdentityUser IdentityUser { get; set; }
    public Rank Rank { get; init; }
}