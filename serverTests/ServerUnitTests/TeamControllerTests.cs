using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ping_Map_Play_pong.Controllers;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Service;



namespace ServerUnitTests;

public class TeamControllerTests
{
    private Mock<ILogger<TeamController>> _loggerMock;
    private Mock<ITeamService> _teamServiceMock;
    private TeamController _teamController;

    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger<TeamController>>();
        _teamServiceMock = new Mock<ITeamService>();

        _teamController = new TeamController(_loggerMock.Object, _teamServiceMock.Object);
    }
    
    
    [Test]
    public void GetAll_ReturnsTeams()
    {
        // Arrange
        var teams = new List<Team>
        {
            new Team { Id = 1},
            new Team { Id = 2}
        };
        _teamServiceMock.Setup(service => service.GetAll()).Returns(teams.AsQueryable());

        // Act
        var result = _teamController.GetAll();

        // Assert
        Assert.IsNotNull(result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual(teams, okResult.Value);
    }
    

    [Test]
    public void GetAll_ReturnsOk_WhenNoTeams()
    {
        // Arrange
        _teamServiceMock.Setup(service => service.GetAll()).Returns(new List<Team>().AsQueryable());

        // Act
        var result = _teamController.GetAll();

        // Assert
        Assert.IsNotNull(result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.IsInstanceOf<IEnumerable<Team>>(okResult.Value);
        Assert.IsEmpty((IEnumerable<Team>)okResult.Value);
    }
    
    [Test]
    public void GetByUserId_ReturnsTeams()
    {
        // Arrange
        int userId = 1;
        var teams = new List<Team>
        {
            new Team { Id = 1},
            new Team { Id = 2}
        };
        _teamServiceMock.Setup(service => service.GetByUserId(userId)).Returns(teams.AsQueryable());

        // Act
        var result = _teamController.GetByUserId(userId);

        // Assert
        Assert.IsNotNull(result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual(teams, okResult.Value);
    }

    [Test]
    public void GetByUserId_ReturnsNotFound_WhenNoTeams()
    {
        // Arrange
        int userId = 1;
        _teamServiceMock.Setup(service => service.GetByUserId(userId)).Throws(new Exception("No teams found for user"));

        // Act
        var result = _teamController.GetByUserId(userId);

        // Assert
        Assert.IsNotNull(result);
        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual(404, notFoundResult.StatusCode);
        Assert.AreEqual($"team with id:{userId} not exist in DB", notFoundResult.Value);
    }

    
    [Test]
    public void GetByPlayersId_ReturnsNotFound_WhenNoTeamFound()
    {
        // Arrange
        int player1Id = 1;
        int player2Id = 2;
        _teamServiceMock.Setup(service => service.GetByPlayersId(player1Id, player2Id)).Throws(new Exception("Team not found for players"));

        // Act
        var result = _teamController.GetByPlayersId(player1Id, player2Id);

        // Assert
        Assert.IsNotNull(result);
        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual(404, notFoundResult.StatusCode);
        Assert.AreEqual($"team with players:{player1Id} and {player2Id} not exist in DB", notFoundResult.Value);
    }

    [Test]
    public void Post_ReturnsBadRequest_WhenTeamAlreadyExists()
    {
        // Arrange
        int player1Id = 10;
        int player2Id = 20;

        _teamServiceMock.Setup(service => service.GetByPlayersId(player1Id, player2Id)).Returns(new Team());

        // Act
        var result = _teamController.Post(player1Id, player2Id);

        // Assert
        Assert.IsNotNull(result, "Result is null");
        var badRequestResult = result.Result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult, "Result is not BadRequestObjectResult");
        Assert.AreEqual(400, badRequestResult.StatusCode, "Status code is not 400");
        Assert.AreEqual("This team already exist", badRequestResult.Value, "Returned message is not as expected");

        _teamServiceMock.Verify(service => service.PostToDb(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
    }


    
    [Test]
public void Delete_ReturnsOk_OnSuccessfulDeletion()
{
    // Arrange
    int teamId = 1;

    _teamServiceMock.Setup(service => service.DeleteFromDb(teamId));

    // Act
    var result = _teamController.Delete(teamId);

    // Assert
    Assert.IsNotNull(result, "Result is null");
    var okResult = result.Result as OkObjectResult;
    Assert.IsNotNull(okResult, "Result is not OkObjectResult");
    Assert.AreEqual(200, okResult.StatusCode, "Status code is not 200");
    Assert.AreEqual("successful delete", okResult.Value, "Returned message is not as expected");


    _teamServiceMock.Verify(service => service.DeleteFromDb(teamId), Times.Once);
}

    [Test]
    public void Delete_ReturnsNotFound_WhenTeamDoesNotExist()
    {
        // Arrange
        int teamId = 1;
        _teamServiceMock.Setup(service => service.DeleteFromDb(teamId)).Throws(new Exception("Team not found"));

        // Act
        var result = _teamController.Delete(teamId);

        // Assert
        Assert.IsNotNull(result, "Result is null");
        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult, "Result is not NotFoundObjectResult");
        Assert.AreEqual(404, notFoundResult.StatusCode, "Status code is not 404");
        Assert.AreEqual($"team with id:{teamId} not exist in DB", notFoundResult.Value, "Returned message is not as expected");
       
    }
}