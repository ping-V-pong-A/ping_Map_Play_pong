namespace ping_Map_Play_pong.Model.RequestModels;

public class PairMatchRequest
{
    public int TableId { get; set; }
    public int Team1Player1Id { get; set; }
    public int Team1Player2Id { get; set; }
    public int Team1Point { get; set; }
    public int Team2Player1Id { get; set; }
    public int Team2Player2Id { get; set; }
    public int Team2Point { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}