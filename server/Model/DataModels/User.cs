using System.ComponentModel.DataAnnotations;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.Enums;

namespace ping_Map_Play_pong.Model;

public class User
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime RegistrationDate { get; set; }
    public List<Table> CheckedInTables { get; }
    public Rank Rank { get; set; }
}