using Microsoft.AspNetCore.Identity;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.Enums;

namespace ping_Map_Play_pong.Model.RequestModels;

public class UserRequest
{
    public ICollection<Table> CheckedInTables { get; set; }

    public string IdentityUserEmail { get; set; }
    
    public IdentityUser IdentityUser { get; set; }
    
    public Rank Rank { get; set; }
}