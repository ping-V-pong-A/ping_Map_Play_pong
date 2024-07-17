using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ping_Map_Play_pong.Data;


namespace IntegrationTests;

public class SolarWatchWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _dbName = Guid.NewGuid().ToString();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {

            var pingPongDbContextDescriptor =
                services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<PingMapPlayPongContext>));

            services.Remove(pingPongDbContextDescriptor);

            services.AddDbContext<PingMapPlayPongContext>(options => { options.UseInMemoryDatabase(_dbName); });

            using var scope = services.BuildServiceProvider().CreateScope();


            var solarContext = scope.ServiceProvider.GetRequiredService<PingMapPlayPongContext>();
            solarContext.Database.EnsureDeleted();
            solarContext.Database.EnsureCreated();

        });
    }
}