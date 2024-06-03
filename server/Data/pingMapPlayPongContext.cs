using Microsoft.EntityFrameworkCore;
using ping_Map_Play_pong.Model;
using ping_Map_Play_pong.Model.DataModels;
using Table = Microsoft.EntityFrameworkCore.Metadata.Internal.Table;

namespace ping_Map_Play_pong.Data;

public class pingMapPlayPongContext : DbContext
{
    public DbSet<CheckingIn> CheckingIns { get; set; }
    public DbSet<Coordinate> Coordinates { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<User> Users { get; set; }
    private readonly IConfiguration _configuration;

    public pingMapPlayPongContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
    {
        optionBuilder.UseSqlServer(_configuration.GetConnectionString("MSSQL_CONNECTION"));
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasIndex(u =>  u.Email )
            .IsUnique();
        
        builder.Entity<User>()
            .HasIndex(u =>  u.Name )
            .IsUnique();
        
        builder.Entity<User>()
            .HasMany(u => u.CheckedInTables);
            

    }
}