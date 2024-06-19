using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.Enums;

namespace ping_Map_Play_pong.Service.Authentication;

public record AuthResult(
    bool Success,
    string Email,
    string UserName,
    string Token,
    DateTime? RegistrationDate = null,
    ICollection<Table> CheckedInTables = null, 
    Rank Rank = Rank.None )


{
//Error code - error message
public readonly Dictionary<string, string> ErrorMessages = new();
}