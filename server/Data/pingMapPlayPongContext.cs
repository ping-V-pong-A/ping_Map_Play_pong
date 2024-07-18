using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using ping_Map_Play_pong.Model;
using ping_Map_Play_pong.Model.DataModels;
namespace ping_Map_Play_pong.Data;

public class PingMapPlayPongContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public DbSet<User> Users { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<CheckingIn> CheckingIns { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<Coordinate> Coordinates { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<PairMatch> PairMatches { get; set; }
    
    private readonly IConfiguration _configuration;


    public PingMapPlayPongContext(DbContextOptions<PingMapPlayPongContext> options,IConfiguration configuration)  : base(options)
    {
        _configuration = configuration;

        if (Database.GetService<IDatabaseCreator>() is RelationalDatabaseCreator databaseCreator)
        {
            if (!databaseCreator.CanConnect()) databaseCreator.Create();
            if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
        }
    }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>()
            .HasOne(u => u.IdentityUser)
            .WithMany()
            .HasForeignKey(u => u.IdentityUserEmail)
            .HasPrincipalKey(iu => iu.Email); // Az IdentityUser osztály Email oszlopára hivatkozunk

    }
}