using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ping_Map_Play_pong.Controllers;
using ping_Map_Play_pong.Model;
using ping_Map_Play_pong.Model.RequestModels;
using ping_Map_Play_pong.Service;
using ping_Map_Play_pong.Model.Exceptions;
using Match = ping_Map_Play_pong.Model.DataModels.Match;

namespace ServerUnitTests
{
    public class MatchControllerTests
    {
        private Mock<ILogger<MatchController>> _loggerMock;
        private Mock<IMatchService> _matchServiceMock;
        private MatchController _matchController;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<MatchController>>();
            _matchServiceMock = new Mock<IMatchService>();
            _matchController = new MatchController(_loggerMock.Object, _matchServiceMock.Object);
        }

        [Test]
        public void GetAll_ReturnsMatches()
        {
            // Arrange
            var matches = new List<Match>
            {
                new Match { Id = 1, TableId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(1), Player1 = new User { Id = 1}, Player1Point = 10, Player2 = new User { Id = 2}, Player2Point = 5 },
                new Match { Id = 2, TableId = 2, StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(1), Player1 = new User { Id = 3}, Player1Point = 15, Player2 = new User { Id = 4}, Player2Point = 10 }
            };
            _matchServiceMock.Setup(service => service.GetAll()).Returns(matches.AsQueryable());

            // Act
            var result = _matchController.GetAll();

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult, "Expected OkObjectResult");
            Assert.AreEqual(200, okResult.StatusCode);
            
        }

        [Test]
        public void GetAll_ReturnsNotFound_WhenNoMatches()
        {
            // Arrange
            _matchServiceMock.Setup(service => service.GetAll()).Returns(new List<Match>().AsQueryable());

            // Act
            var result = _matchController.GetAll();

            // Assert
            var notFoundResult = result.Result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult, "Expected NotFoundObjectResult");
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual("matches table is empty", notFoundResult.Value);
        }

        [Test]
        public void GetById_ReturnsRightMatch()
        {
            // Arrange
            var matchId = 1;
            var expectedMatch = new Match
            {
                Id = matchId,
                TableId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(1),
                Player1 = new User { Id = 1},
                Player1Point = 10,
                Player2 = new User { Id = 2},
                Player2Point = 5
            };

            _matchServiceMock.Setup(service => service.GetById(matchId)).Returns(expectedMatch);

            // Act
            var result = _matchController.GetById(matchId);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult, "Expected OkObjectResult");
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(expectedMatch, okResult.Value);
        }

        [Test]
        public void GetById_ReturnsNotFound_WhenMatchDoesNotExist()
        {
            // Arrange
            var matchId = 1;
            _matchServiceMock.Setup(service => service.GetById(matchId)).Returns((Match)null);

            // Act
            var result = _matchController.GetById(matchId);

            // Assert
            var notFoundResult = result.Result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult, "Expected NotFoundObjectResult");
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual($"match with id:{matchId} not exist in DB", notFoundResult.Value);
        }

        [Test]
        public void GetByUserId_ReturnsMatches()
        {
            // Arrange
            var userId = 1;
            var matches = new List<Match>
            {
                new Match { Id = 1, TableId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(1), Player1 = new User { Id = userId}, Player1Point = 10, Player2 = new User { Id = 2}, Player2Point = 5 },
                new Match { Id = 2, TableId = 2, StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(1), Player1 = new User { Id = 3}, Player1Point = 15, Player2 = new User { Id = userId}, Player2Point = 10 }
            };
            _matchServiceMock.Setup(service => service.GetByUserId(userId)).Returns(matches.AsQueryable());

            // Act
            var result = _matchController.GetByUserId(userId);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult, "Expected OkObjectResult");
            Assert.AreEqual(200, okResult.StatusCode);
            
        }

        [Test]
        public void GetByPlayersId_ReturnsMatches()
        {
            // Arrange
            var player1Id = 1;
            var player2Id = 2;
            var matches = new List<Match>
            {
                new Match { Id = 1, TableId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(1), Player1 = new User { Id = player1Id}, Player1Point = 10, Player2 = new User { Id = player2Id}, Player2Point = 5 },
                new Match { Id = 2, TableId = 2, StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(1), Player1 = new User { Id = player1Id}, Player1Point = 15, Player2 = new User { Id = player2Id}, Player2Point = 10 }
            };
            _matchServiceMock.Setup(service => service.GetByPlayersId(player1Id, player2Id)).Returns(matches.AsQueryable());

            // Act
            var result = _matchController.GetByPlayersId(player1Id, player2Id);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult, "Expected OkObjectResult");
            Assert.AreEqual(200, okResult.StatusCode);
            
        }

        [Test]
        public void GetByPlayersId_ReturnsBadRequest_OnException()
        {
            // Arrange
            var player1Id = 1;
            var player2Id = 2;
            
            _matchServiceMock.Setup(service => service.GetByPlayersId(player1Id, player2Id))
                .Throws(new ArgumentException("Players not found"));

            // Act
            var result = _matchController.GetByPlayersId(player1Id, player2Id);

            // Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult, "Expected BadRequestObjectResult");
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("something went wrong", badRequestResult.Value);
        }

        [Test]
        public void GetByDate_ReturnsMatches_OnValidDate()
        {
            // Arrange
            var date = new DateTime(2024, 07, 17);
            var expectedMatches = new List<Match>
            {
                new Match { Id = 1, StartDate = date},
                new Match { Id = 2, StartDate = date}
            };
            _matchServiceMock.Setup(service => service.GetByDate(date)).Returns(expectedMatches);

            // Act
            var result = _matchController.GetByDate(date);

            // Assert
            var okObjectResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okObjectResult, "Expected OkObjectResult");
            Assert.AreEqual(200, okObjectResult.StatusCode);
            var returnedMatches = okObjectResult.Value as IEnumerable<Match>;
            Assert.IsNotNull(returnedMatches, "Returned value is not IEnumerable<Match>");
            Assert.AreEqual(expectedMatches.Count, returnedMatches.Count(), "Number of matches does not match");
            CollectionAssert.AreEqual(expectedMatches, returnedMatches, "Returned matches are not as expected");
        }

        
        
        
        [Test]
        public void Post_ReturnsOk_OnSuccessfulAddition()
        {
            // Arrange
            var request = new MatchRequest
            {
                TableId = 1,
                Player1Id = 10,
                Player2Id = 20,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddHours(1)
            };

            
            _matchServiceMock.Setup(service => service.PostToDb(It.IsAny<MatchRequest>()));

            // Act
            var result = _matchController.Post(request);

            // Assert
            Assert.IsNotNull(result, "Expected a result.");
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult, "Expected OkObjectResult");
            Assert.AreEqual(200, okResult.StatusCode, "Expected status code 200");
            Assert.AreEqual("success added new match", okResult.Value, "Expected success message did not match");
        }

        
        
        

        [Test]
        public void Post_ReturnsBadRequest_OnFailedAddition()
        {
            // Arrange
            var request = new MatchRequest
            {
                TableId = 1,
                Player1Id = 10,
                Player2Id = 20,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddHours(1)
            };

            
            _matchServiceMock.Setup(service => service.PostToDb(request))
                .Throws(new NotFoundException("Match could not be added"));

            // Act
            var result = _matchController.Post(request);

            // Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult, "Expected BadRequestObjectResult");
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("Match could not be added", badRequestResult.Value);
        }


    }
}