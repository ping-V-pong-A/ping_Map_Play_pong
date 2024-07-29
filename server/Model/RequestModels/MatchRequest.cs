namespace ping_Map_Play_pong.Model.RequestModels;

public class MatchRequest
{
   public int Id { get; init; }
   public int TableId { get; init; }
   public DateTime StartDate { get; init; }
   public DateTime EndDate { get; init; }
   public int Player1Id { get; init; }
   public int Player1Point { get; init; }
   public int Player2Id { get; init; }
   public int Player2Point { get; init; }
}