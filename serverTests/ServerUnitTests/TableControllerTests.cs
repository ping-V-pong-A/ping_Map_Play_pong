
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ping_Map_Play_pong.Controllers;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.ResponseModels;
using NUnit.Framework;
using ping_Map_Play_pong.Model.Exceptions;
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
                Id = 1,
                Name = "Table 1",
                Coordinate = new Coordinate { Lat = 47.1234, Lon = 19.5678 },
                LeaderBoard = new List<Match>(),
                PairMatchesLeaderBoard = new List<PairMatch>(),
                CheckingIns = new List<CheckingIn>()
            },
            new Table
            {
                Id = 2,
                Name = "Table 2",
                Coordinate = new Coordinate { Lat = 48.1234, Lon = 20.5678 },
                LeaderBoard = new List<Match>(),
                PairMatchesLeaderBoard = new List<PairMatch>(),
                CheckingIns = new List<CheckingIn>()
            }
        };

        
        var tableResponses = tables.Select(table => new TableResponse
        {
            Id = table.Id,
            Name = table.Name,
            Lat = table.Coordinate.Lat,
            Lon = table.Coordinate.Lon,
            CheckingIns = table.CheckingIns.ToList(),
            Matches = table.LeaderBoard.ToList(),
            PairMatches = table.PairMatchesLeaderBoard.ToList()
        }).ToList();

        // Mock setup
        _tableServiceMock.Setup(service => service.GetAll()).Returns(tableResponses);
        
        // Act
        var result = _tableController.GetAll();

        // Assert
        Assert.IsNotNull(result, "Result is null");
        var okObjectResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okObjectResult, "Result is not OkObjectResult");

        var responseTables = okObjectResult.Value as IEnumerable<TableResponse>;
        Assert.IsNotNull(responseTables, "Response is not IEnumerable<TableResponse>");
        Assert.AreEqual(2, responseTables.Count(), "Expected 2 tables in response");
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
        _tableServiceMock.Setup(service => service.GetById(tableId))
            .Throws(new NotFoundException($"table with id:{tableId} not exist in DB"));

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

      
        _tableServiceMock.Setup(service => service.PostToDb(It.IsAny<TableRequest>()))
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
            Coordinate = new Coordinate { Lat = 47.1234, Lon = 19.5678 }
        };

       
        _tableServiceMock.Setup(service => service.GetById(tableId)).Returns(existingTable);

        
        _tableServiceMock.Setup(service => service.Update(tableId, updatedRequest)).Verifiable();

        // Act
        var result = _tableController.Update(tableId, updatedRequest);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        
    
      
        _tableServiceMock.Verify(service => service.Update(tableId, updatedRequest), Times.Once);
    }



    [Test]
    public void Patch_ReturnsBadRequest_IfUpdateFails()
    {
        // Arrange
        var tableId = 1;
        var request = new TableRequest { Name = "Updated Name", Lat = 12.345, Lon = 45.678 };
    
        
        var existingTable = new Table
        {
            Id = tableId,
            Name = "Old Name",
            Coordinate = new Coordinate { Lat = 10.0, Lon = 20.0 }
        };

        _tableServiceMock.Setup(service => service.GetById(tableId)).Returns(existingTable);
    
        // Setup Update to throw an exception
        _tableServiceMock.Setup(service => service.Update(tableId, It.IsAny<TableRequest>()))
            .Throws(new Exception("Update failed"));

        // Act
        var result = _tableController.Update(tableId, request);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
        var badRequestResult = result as BadRequestObjectResult;
        Assert.AreEqual(400, badRequestResult.StatusCode);
        Assert.IsNotNull(badRequestResult.Value);
        Assert.AreEqual("Update failed", badRequestResult.Value.ToString());
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

        
        _tableServiceMock.Setup(service => service.GetById(tableId))
            .Throws(new NotFoundException($"table with id:{tableId} not exist in DB"));

        // Act
        var result = _tableController.Update(tableId, updatedRequest);

        // Assert
        var notFoundResult = result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual(404, notFoundResult.StatusCode);
        Assert.AreEqual($"table with id:{tableId} not exist in DB", notFoundResult.Value);
    }




}
