using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.Enums;

namespace ping_Map_Play_pong.Model.ResponseModels;

public class UserResponse
{
    public int Id { get; init; }
    public string UserName { get; set; }
    public Rank Rank { get; init; }
    public DateTime RegistrationDate { get; init; }
    public ICollection<Table> CheckedInTables { get; init; } = new List<Table>();
}
