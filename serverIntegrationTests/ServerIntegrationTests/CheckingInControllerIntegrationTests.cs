using System.Net;
using System.Text;
using Newtonsoft.Json;
using ping_Map_Play_pong.Contracts;

namespace IntegrationTests;

[Collection("IntegrationTests")]

public class CheckingInControllerIntegrationTests:IClassFixture<SolarWatchWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly SolarWatchWebApplicationFactory _factory;

    public CheckingInControllerIntegrationTests(SolarWatchWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task GetAll_ShouldReturnCheckingIns()
    {
        // Arrange
        var requestUri = "/api/check-ins";

        // Act
        var response = await _client.GetAsync(requestUri);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
       
    }

    [Fact]
    public async Task Delete_ShouldRemoveCheckingIn()
    {
        // Arrange
        var checkingInIdToDelete = 1;
        var requestUri = $"/api/check-ins/delete/{checkingInIdToDelete}";

        // Act
        var response = await _client.DeleteAsync(requestUri);

        // Assert
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
        
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        else
        {
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Assert.Contains("successful delete", content);
         
        }
    }
   
}
    
