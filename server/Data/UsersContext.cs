using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ping_Map_Play_pong.Data;

public class UsersContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    private readonly IConfiguration _configuration;
    
    public UsersContext (DbContextOptions<UsersContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = _configuration.GetConnectionString("MSSQL_CONNECTION");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}