using System.ComponentModel.DataAnnotations;

namespace ping_Map_Play_pong.Model.DataModels;

public class Coordinate
{
    [Key]
    public int Id { get; init; }
    public double Lat { get; init; } 
    public double Lon { get; init; }
}

