namespace ping_Map_Play_pong.Model.RequestModels;

public class CheckInRequest
{
    public int UserId { get; set; }
    public int TableId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}