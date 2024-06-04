using Microsoft.EntityFrameworkCore;
using ping_Map_Play_pong.Model;
using ping_Map_Play_pong.Model.DataModels;
}
namespace ping_Map_Play_pong.Data;

public class pingMapPlayPongContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<CheckingIn> CheckingIns { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<Coordinate> Coordinates { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<PairMatch> PairMatches { get; set; }
    
    private readonly IConfiguration _configuration;

    public pingMapPlayPongContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
    {
        optionBuilder.UseSqlServer(_configuration.GetConnectionString("MSSQL_CONNECTION"));
    }
}