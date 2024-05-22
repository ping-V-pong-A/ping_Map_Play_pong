using System.Runtime.InteropServices.JavaScript;

namespace ping_Map_Play_pong.Model;

public class CheckingIn
{
    public User User{ get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}