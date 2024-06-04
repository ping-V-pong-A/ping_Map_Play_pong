using System.ComponentModel.DataAnnotations;

namespace ping_Map_Play_pong.Model.DataModels;

public class Coordinate
{
    [Key]
    public int Id { get; set; }
    public double Lat { get; set; } 
    public double Lon { get; set; }
}

