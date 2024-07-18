using System.Net;
using System.Text;
using Newtonsoft.Json;
using ping_Map_Play_pong.Contracts;

namespace IntegrationTests;

[Collection("IntegrationTests")]

public class TeamControllerIntegrationTests:IClassFixture<SolarWatchWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly SolarWatchWebApplicationFactory _factory;

    public TeamControllerIntegrationTests(SolarWatchWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task TeamController_GetByUserId_ReturnsTeams()
    {
        // Arrange
        var userId = 1;

        // Act
        var response = await _client.GetAsync($"/api/teams/{userId}");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
    }
        
        
    [Fact]
    public async Task TeamController_AddTeam_Success()
    {
        // Arrange
        var request = new { player1Id = 1, player2Id = 2 };
        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/teams/add", content);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
    }

}