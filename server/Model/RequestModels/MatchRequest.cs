namespace ping_Map_Play_pong.Model.RequestModels;

public class MatchRequest
{
   public int TableId { get; set; }
   public int Player1Id { get; set; }
   public int Player2Id { get; set; }
   public DateTime StartTime { get; set; }
   public DateTime EndTime { get; set; }
}