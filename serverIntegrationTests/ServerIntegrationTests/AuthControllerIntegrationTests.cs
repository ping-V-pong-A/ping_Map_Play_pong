using System.Net;
using System.Text;
using Newtonsoft.Json;
using ping_Map_Play_pong.Contracts;

namespace IntegrationTests;

[Collection("IntegrationTests")]

public class AuthControllerIntegrationTests:IClassFixture<SolarWatchWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly SolarWatchWebApplicationFactory _factory;

    public AuthControllerIntegrationTests(SolarWatchWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }
    
    
    [Fact]
    public async Task AuthController_Register_Success()
    {
        // Arrange
        var request = new RegistrationRequest("email@email.hu", "testUser", "password123");
           
        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/auth/sign-up", content);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            
    }
}