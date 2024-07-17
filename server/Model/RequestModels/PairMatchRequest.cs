namespace ping_Map_Play_pong.Model.RequestModels;

public class PairMatchRequest
{
    public int TableId { get; set; }
    public int Team1Player1Id { get; set; }
    public int Team1Player2Id { get; set; }
    public int Team2Player1Id { get; set; }
    public int Team2Player2Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}