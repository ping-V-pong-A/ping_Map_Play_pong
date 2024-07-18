using System.Net;
using System.Text;
using Newtonsoft.Json;
using ping_Map_Play_pong.Contracts;

namespace IntegrationTests;

[Collection("IntegrationTests")]
public class UserControllerIntegrationTests: IClassFixture<SolarWatchWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly SolarWatchWebApplicationFactory _factory;

    public UserControllerIntegrationTests(SolarWatchWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task UserController_GetAll_Success()
    {
        // Act
        var response = await _client.GetAsync("/api/users");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

}