
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ping_Map_Play_pong.Controllers;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.ResponseModels;
using NUnit.Framework;
using ping_Map_Play_pong.Model.RequestModels;
using ping_Map_Play_pong.Service;
using Match = ping_Map_Play_pong.Model.DataModels.Match;

namespace ServerUnitTests;

public class TableControllerTests
{
    private Mock<ILogger<TableController>> _loggerMock;
    private Mock<ITableService> _tableServiceMock;
    private TableController _tableController;

    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger<TableController>>();
        _tableServiceMock = new Mock<ITableService>();
        _tableController = new TableController(_loggerMock.Object, _tableServiceMock.Object);
    }


    [Test]
    public void GetAll_ReturnsListOfTables()
    {
        // Arrange
        var tables = new List<Table>
        {
            new Table
            {
                Id = 1, Name = "Table 1", Coordinate = new Coordinate { Lat = 47.1234, Lon = 19.5678 },
                LeaderBoard = new List<Match>(), PairMatchesLeaderBoard = new List<PairMatch>(),
                CheckingIns = new List<CheckingIn>()
            },
            new Table
            {
                Id = 2, Name = "Table 2", Coordinate = new Coordinate { Lat = 48.1234, Lon = 20.5678 },
                LeaderBoard = new List<Match>(), PairMatchesLeaderBoard = new List<PairMatch>(),
                CheckingIns = new List<CheckingIn>()
            }
        };

        _tableServiceMock.Setup(service => service.GetAll()).Returns(tables);

        // Act
        var result = _tableController.GetAll();

        // Assert
        Assert.IsNotNull(result, "Result is null");
        var okObjectResult = result.Result as OkObjectResult;


    }

    [Test]
    public void GetById_ReturnsTable_WhenTableExists()
    {
        // Arrange
        var tableId = 1;
        var table = new Table
        {
            Id = tableId, Name = "Table 1", Coordinate = new Coordinate { Lat = 47.1234, Lon = 19.5678 },
            LeaderBoard = Enumerable.Empty<Match>().ToList(),
            PairMatchesLeaderBoard = Enumerable.Empty<PairMatch>().ToList(),
            CheckingIns = Enumerable.Empty<CheckingIn>().ToList()
        };
        _tableServiceMock.Setup(service => service.GetById(tableId)).Returns(table);

        // Act
        var result = _tableController.GetById(tableId);

        // Assert
        Assert.IsNotNull(result, "Result is null");

        var okObjectResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okObjectResult, "Result is not OkObjectResult");
        Assert.AreEqual(200, okObjectResult.StatusCode, "Status code is not 200");


    }


    [Test]
    public void GetById_ReturnsNotFound_WhenTableDoesNotExist()
    {
        // Arrange
        var tableId = 1;
        _tableServiceMock.Setup(service => service.GetById(tableId)).Throws(new Exception("Table not found"));

        // Act
        var result = _tableController.GetById(tableId);

        // Assert
        Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
        var notFoundResult = result.Result as NotFoundObjectResult;
        Assert.AreEqual(404, notFoundResult.StatusCode);
        Assert.AreEqual($"table with id:{tableId} not exist in DB", notFoundResult.Value);
    }

    [Test]
    public async Task GetById_ReturnsTable_IfTableExists()
    {
        // Arrange
        var tableId = 1;
        var expectedTable = new Table { Id = tableId, Name = "Table 1" };
        _tableServiceMock.Setup(service => service.GetById(tableId)).Returns(expectedTable);

        // Act
        var result = _tableController.GetById(tableId);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreSame(expectedTable, okResult.Value);
    }


    [Test]
    public void Post_ValidTable_ReturnsOk()
    {
        // Arrange
        var tableRequest = new TableRequest
        {
            Name = "New Table",
            Lat = 30.0,
            Lon = 40.0
        };

        // Act
        var result = _tableController.Post(tableRequest);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okObjectResult = result as OkObjectResult;
        Assert.AreEqual(200, okObjectResult.StatusCode);

    }
    
    [Test]
    public void Post_ServiceException_ReturnsBadRequest()
    {
        // Arrange
        var invalidRequest = new TableRequest
        {
            Name = "Table 1",
            Lat = 47.1234,
            Lon = 19.5678
        };

        _tableServiceMock.Setup(service => service.PostToDb(It.IsAny<Table>()))
            .Throws(new Exception("Service exception"));

        // Act
        var result = _tableController.Post(invalidRequest);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
        var badRequestResult = result as BadRequestObjectResult;
        Assert.AreEqual(400, badRequestResult.StatusCode);

    }


    [Test]
    public void Patch_ExistingId_ValidRequest_ReturnsOk()
    {
        // Arrange
        var tableId = 1;
        var updatedRequest = new TableRequest
        {
            Name = "Updated Table",
            Lat = 35.0,
            Lon = 45.0
        };

        var existingTable = new Table
        {
            Id = tableId,
            Name = "Table 1",
            Coordinate = new Coordinate { Lat = 47.1234, Lon = 19.5678 },
            LeaderBoard = new List<Match>(),
            PairMatchesLeaderBoard = new List<PairMatch>(),
            CheckingIns = new List<CheckingIn>()
        };

        _tableServiceMock.Setup(service => service.GetById(tableId)).Returns(existingTable);

        // Act
        var result = _tableController.Patch(tableId, updatedRequest);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okObjectResult = result as OkObjectResult;
        Assert.AreEqual(200, okObjectResult.StatusCode);

    }


    [Test]
    public void Patch_ReturnsBadRequest_IfUpdateFails()
    {
        // Arrange
        var tableId = 1;
        var request = new TableRequest { Name = "Updated Name", Lat = 12.345, Lon = 45.678 };
        _tableServiceMock.Setup(service => service.GetById(tableId)).Returns(new Table());
        _tableServiceMock.Setup(service => service.Update(It.IsAny<Table>())).Throws(new Exception("Update failed"));

        // Act
        var result = _tableController.Patch(tableId, request);

        // Assert
     
        var badRequestResult = result as BadRequestObjectResult;
        Assert.AreEqual(400, badRequestResult.StatusCode);

    
    }


    [Test]
    public void Delete_ExistingId_ReturnsOk()
    {
        // Arrange
        var tableId = 1;

        // Act
        var result = _tableController.Delete(tableId);

        // Assert
        Assert.IsInstanceOf<ActionResult<string>>(result);
        var actionResult = result as ActionResult<string>;
        Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
        var okObjectResult = actionResult.Result as OkObjectResult;
        Assert.AreEqual(200, okObjectResult.StatusCode);
        Assert.AreEqual("successful delete", okObjectResult.Value);
    }




    [Test]
    public void Patch_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        var tableId = 1;
        var updatedRequest = new TableRequest
        {
            Name = "Updated Table",
            Lat = 35.0,
            Lon = 45.0
        };

        _tableServiceMock.Setup(service => service.GetById(tableId)).Returns((Table)null);

        // Act
        var result = _tableController.Patch(tableId, updatedRequest);

        // Assert
        Assert.IsInstanceOf<NotFoundObjectResult>(result);
        var notFoundResult = result as NotFoundObjectResult;
        Assert.AreEqual(404, notFoundResult.StatusCode);
        Assert.AreEqual($"Table with id:{tableId} not found", notFoundResult.Value);
    }


}

