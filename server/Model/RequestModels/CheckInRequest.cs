namespace ping_Map_Play_pong.Model.RequestModels;

public class CheckInRequest
{
    public int UserId { get; set; }
    public int TableId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}