using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ping_Map_Play_pong.Controllers;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.RequestModels;
using ping_Map_Play_pong.Service;



namespace ServerUnitTests;

public class PairMatchControllerTests
{
    private Mock<ILogger<PairMatchController>> _loggerMock;
    private Mock<IPairMatchService> _pairMatchServiceMock;
    private PairMatchController _pairMatchController;

    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger<PairMatchController>>();
        _pairMatchServiceMock = new Mock<IPairMatchService>();
        _pairMatchController = new PairMatchController(_loggerMock.Object, _pairMatchServiceMock.Object);
    }
    
    
       [Test]
        public void GetAll_Returns_OkResult_With_PairMatches()
        {
            // Arrange
            var expectedPairMatches = new List<PairMatch>
            {
                new PairMatch { Id = 1, TableId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(1), Team1Point = 2, Team2Point = 1 },
                new PairMatch { Id = 2, TableId = 2, StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(1), Team1Point = 3, Team2Point = 4 }
            };
            _pairMatchServiceMock.Setup(x => x.GetAll()).Returns(expectedPairMatches);

            // Act
            var result = _pairMatchController.GetAll();

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var pairMatches = okResult.Value as IEnumerable<PairMatch>;
            Assert.NotNull(pairMatches);
            Assert.AreEqual(expectedPairMatches.Count, pairMatches.Count());
        }

        [Test]
        public void GetAll_Returns_NotFound_When_Service_Throws_Exception()
        {
            // Arrange
            _pairMatchServiceMock.Setup(x => x.GetAll()).Throws(new Exception("Simulated error"));

            // Act
            var result = _pairMatchController.GetAll();

            // Assert
            var notFoundResult = result.Result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual("pairPairMatches table is empty", notFoundResult.Value);
        }

   
        [Test]
        public void GetAll_Returns_Ok_When_No_PairMatches_In_Database()
        {
            // Arrange
            var emptyList = new List<PairMatch>();

            _pairMatchServiceMock.Setup(x => x.GetAll()).Returns(emptyList);

            // Act
            var result = _pairMatchController.GetAll();

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.NotNull(okResult);

            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(emptyList, okResult.Value);
        }


        [Test]
        public void GetByDate_ReturnsMatches_OnValidDate()
        {
            // Arrange
            var date = new DateTime(2024, 07, 17);
            var expectedMatches = new List<PairMatch>
            {
                new PairMatch { Id = 1, StartDate = date },
                new PairMatch { Id = 2, StartDate = date }
            };
            _pairMatchServiceMock.Setup(service => service.GetByDate(date)).Returns(expectedMatches);

            // Act
            var result = _pairMatchController.GetByDate(date);

            // Assert
            Assert.IsNotNull(result, "Result is null");
            var okObjectResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okObjectResult, "Result is not OkObjectResult");
            Assert.AreEqual(200, okObjectResult.StatusCode, "Status code is not 200");
            var returnedMatches = okObjectResult.Value as IEnumerable<PairMatch>;
            Assert.IsNotNull(returnedMatches, "Returned value is not IEnumerable<Match>");
            Assert.AreEqual(expectedMatches.Count, returnedMatches.Count(), "Number of matches does not match");
            CollectionAssert.AreEqual(expectedMatches, returnedMatches, "Returned matches are not as expected");
        }
        
        
        
        [Test]
        public void GetByDate_ReturnsEmptyList_OnValidDateWithoutMatches()
        {
            // Arrange
            var date = new DateTime(2024, 07, 17);
            var expectedMatches = new List<PairMatch>();
            _pairMatchServiceMock.Setup(service => service.GetByDate(date)).Returns(expectedMatches);

            // Act
            var result = _pairMatchController.GetByDate(date);

            // Assert
            Assert.IsNotNull(result, "Result is null");
            var okObjectResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okObjectResult, "Result is not OkObjectResult");
            Assert.AreEqual(200, okObjectResult.StatusCode, "Status code is not 200");
            var returnedMatches = okObjectResult.Value as IEnumerable<PairMatch>;
            Assert.IsNotNull(returnedMatches, "Returned value is not IEnumerable<Match>");
            Assert.AreEqual(expectedMatches.Count, returnedMatches.Count(), "Number of matches does not match");
            CollectionAssert.AreEqual(expectedMatches, returnedMatches, "Returned matches are not as expected");
        }

        
        [Test]
        public void GetById_ReturnsMatch_OnValidId()
        {
            // Arrange
            var pairMatchId = 1;
            var expectedMatch = new PairMatch { Id = pairMatchId, StartDate = new DateTime(2024, 07, 17) };
            _pairMatchServiceMock.Setup(service => service.GetById(pairMatchId)).Returns(expectedMatch);

            // Act
            var result = _pairMatchController.GetById(pairMatchId);

            // Assert
            Assert.IsNotNull(result, "Result is null");
            var okObjectResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okObjectResult, "Result is not OkObjectResult");
            Assert.AreEqual(200, okObjectResult.StatusCode, "Status code is not 200");
            var returnedMatch = okObjectResult.Value as PairMatch;
            Assert.IsNotNull(returnedMatch, "Returned value is not PairMatch");
            Assert.AreEqual(expectedMatch.Id, returnedMatch.Id, "Match ID does not match");
            Assert.AreEqual(expectedMatch.StartDate, returnedMatch.StartDate, "Match StartDate does not match");
        }

        
        [Test]
        public void GetById_ReturnsNotFound_OnInvalidId()
        {
            // Arrange
            var pairMatchId = 1;
            _pairMatchServiceMock.Setup(service => service.GetById(pairMatchId)).Throws(new Exception("Match not found"));

            // Act
            var result = _pairMatchController.GetById(pairMatchId);

            // Assert
            Assert.IsNotNull(result, "Result is null");
            var notFoundResult = result.Result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult, "Result is not NotFoundObjectResult");
            Assert.AreEqual(404, notFoundResult.StatusCode, "Status code is not 404");
            Assert.AreEqual($"pairPairMatch with id:{pairMatchId} not exist in DB", notFoundResult.Value, "NotFound message does not match");
        }

        
        
        [Test]
        public void Post_ReturnsOk_OnSuccessfulAddition()
        {
            // Arrange
            var request = new PairMatchRequest
            {
                TableId = 1,
                Team1Player1Id = 10,
                Team1Player2Id = 20,
                Team2Player1Id = 30,
                Team2Player2Id = 40,
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddHours(1)
            };
            _pairMatchServiceMock.Setup(service => service.PostToDb(request)).Verifiable();

            // Act
            var result = _pairMatchController.Post(request);

            // Assert
            Assert.IsNotNull(result, "Result is null");
            var okObjectResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okObjectResult, "Result is not OkObjectResult");
            Assert.AreEqual(200, okObjectResult.StatusCode, "Status code is not 200");
            Assert.AreEqual("success added new pairPairMatch", okObjectResult.Value, "Returned message is not as expected");


            _pairMatchServiceMock.Verify(service => service.PostToDb(request), Times.Once);
        }
        
        
        
        [Test]
        public void Post_ReturnsBadRequest_OnException()
        {
            // Arrange
            var request = new PairMatchRequest
            {
                TableId = 1,
                Team1Player1Id = 10,
                Team1Player2Id = 20,
                Team2Player1Id = 30,
                Team2Player2Id = 40,
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddHours(1)
            };
            _pairMatchServiceMock.Setup(service => service.PostToDb(request)).Throws(new Exception("Error adding match"));

            // Act
            var result = _pairMatchController.Post(request);

            // Assert
            Assert.IsNotNull(result, "Result is null");
            var badRequestObjectResult = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestObjectResult, "Result is not BadRequestObjectResult");
            Assert.AreEqual(400, badRequestObjectResult.StatusCode, "Status code is not 400");
            Assert.AreEqual("un success added new pairPairMatch", badRequestObjectResult.Value, "Returned message is not as expected");

            
            _pairMatchServiceMock.Verify(service => service.PostToDb(request), Times.Once);
        }


        
        [Test]
        public void Update_ReturnsOk_OnSuccessfulUpdate()
        {
            // Arrange
            var pairMatchId = 1;
            var existingPairMatch = new PairMatch { Id = pairMatchId };
            _pairMatchServiceMock.Setup(service => service.GetById(pairMatchId)).Returns(existingPairMatch);

            // Act
            var result = _pairMatchController.Update(pairMatchId);

            // Assert
            Assert.IsNotNull(result, "Result is null");
            var okObjectResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okObjectResult, "Result is not OkObjectResult");
            Assert.AreEqual(200, okObjectResult.StatusCode, "Status code is not 200");
            Assert.AreEqual("successful update", okObjectResult.Value, "Returned message is not as expected");

            
            _pairMatchServiceMock.Verify(service => service.GetById(pairMatchId), Times.Once);
            _pairMatchServiceMock.Verify(service => service.Update(existingPairMatch), Times.Once);
        }

        
        
        [Test]
        public void Update_ReturnsNotFound_WhenPairMatchDoesNotExist()
        {
            // Arrange
            var pairMatchId = 1;
            _pairMatchServiceMock.Setup(service => service.GetById(pairMatchId)).Returns((PairMatch)null);

            // Act
            var result = _pairMatchController.Update(pairMatchId);

            // Assert
            Assert.IsNotNull(result, "Result is null");
            var notFoundObjectResult = result.Result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundObjectResult, "Result is not NotFoundObjectResult");
            Assert.AreEqual(404, notFoundObjectResult.StatusCode, "Status code is not 404");
            Assert.AreEqual($"match with id:{pairMatchId} not exist in DB", notFoundObjectResult.Value, "Returned message is not as expected");

            
            _pairMatchServiceMock.Verify(service => service.GetById(pairMatchId), Times.Once);
            _pairMatchServiceMock.Verify(service => service.Update(It.IsAny<PairMatch>()), Times.Never);
        }

        
        
        [Test]
        public void Delete_ReturnsOk_OnSuccessfulDelete()
        {
            // Arrange
            var pairMatchId = 1;
            var existingPairMatch = new PairMatch { Id = pairMatchId };
            _pairMatchServiceMock.Setup(service => service.GetById(pairMatchId)).Returns(existingPairMatch);
            _pairMatchServiceMock.Setup(service => service.DeleteFromDb(existingPairMatch)).Verifiable();

            // Act
            var result = _pairMatchController.Delete(pairMatchId);

            // Assert
            Assert.IsNotNull(result, "Result is null");
            var okObjectResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okObjectResult, "Result is not OkObjectResult");
            Assert.AreEqual(200, okObjectResult.StatusCode, "Status code is not 200");
            Assert.AreEqual("successful delete", okObjectResult.Value, "Returned message is not as expected");

           
            _pairMatchServiceMock.Verify(service => service.GetById(pairMatchId), Times.Once);
            _pairMatchServiceMock.Verify(service => service.DeleteFromDb(existingPairMatch), Times.Once);
        }

        
        [Test]
        public void Delete_ReturnsNotFound_WhenPairMatchDoesNotExist()
        {
            // Arrange
            var pairMatchId = 1;
            _pairMatchServiceMock.Setup(service => service.GetById(pairMatchId)).Returns((PairMatch)null);

            // Act
            var result = _pairMatchController.Delete(pairMatchId);

            // Assert
            Assert.IsNotNull(result, "Result is null");
            var notFoundObjectResult = result.Result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundObjectResult, "Result is not NotFoundObjectResult");
            Assert.AreEqual(404, notFoundObjectResult.StatusCode, "Status code is not 404");
            Assert.AreEqual($"match with id:{pairMatchId} not exist in DB", notFoundObjectResult.Value, "Returned message is not as expected");

            
            _pairMatchServiceMock.Verify(service => service.GetById(pairMatchId), Times.Once);
            _pairMatchServiceMock.Verify(service => service.DeleteFromDb(It.IsAny<PairMatch>()), Times.Never);
        }

        
        
        [Test]
        public void Delete_ReturnsBadRequest_OnException()
        {
            // Arrange
            var pairMatchId = 1;
            var existingPairMatch = new PairMatch { Id = pairMatchId };
            _pairMatchServiceMock.Setup(service => service.GetById(pairMatchId)).Returns(existingPairMatch);
            _pairMatchServiceMock.Setup(service => service.DeleteFromDb(existingPairMatch)).Throws(new Exception("Error deleting match"));

            // Act
            var result = _pairMatchController.Delete(pairMatchId);

            // Assert
            Assert.IsNotNull(result, "Result is null");
            var badRequestObjectResult = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestObjectResult, "Result is not BadRequestObjectResult");
            Assert.AreEqual(400, badRequestObjectResult.StatusCode, "Status code is not 400");
            Assert.AreEqual("something went wrong", badRequestObjectResult.Value, "Returned message is not as expected");

           
            _pairMatchServiceMock.Verify(service => service.GetById(pairMatchId), Times.Once);
            _pairMatchServiceMock.Verify(service => service.DeleteFromDb(existingPairMatch), Times.Once);
        }

}




    

    
    
    
    
