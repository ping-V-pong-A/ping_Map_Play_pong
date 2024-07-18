using System.Net;
using System.Text;
using Newtonsoft.Json;
using ping_Map_Play_pong.Contracts;

namespace IntegrationTests;

[Collection("IntegrationTests")]

public class TableControllerIntegrationTests:IClassFixture<SolarWatchWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly SolarWatchWebApplicationFactory _factory;

    public TableControllerIntegrationTests(SolarWatchWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task TableController_GetAll_ReturnsTables()
    {

        // Act
        var response = await _client.GetAsync("/api/tables");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
       
    }

}