using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ping_Map_Play_pong.Controllers;
using ping_Map_Play_pong.Model;
using ping_Map_Play_pong.Model.RequestModels;
using ping_Map_Play_pong.Service;
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
            Assert.IsNotNull(result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(matches, okResult.Value);
        }

        [Test]
        public void GetAll_ReturnsNotFound_WhenNoMatches()
        {
            // Arrange
            _matchServiceMock.Setup(service => service.GetAll()).Returns(new List<Match>().AsQueryable());

            // Act
            var result = _matchController.GetAll();

            // Assert
            Assert.IsNotNull(result);
            var notFoundResult = result.Result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual("matches table is empty", notFoundResult.Value);
        }


        [Test]
        public void GetById_ReturnsRightMatch()
        {
            // Arrange
            var matchId = 1;
            var player1 = new User { Id = 1};
            var player2 = new User { Id = 2}; 

            var expectedMatch = new Match
            {
                Id = matchId,
                TableId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(1),
                Player1 = player1,
                Player1Point = 10,
                Player2 = player2,
                Player2Point = 5
            };

            _matchServiceMock.Setup(service => service.GetById(matchId)).Returns(expectedMatch);

            // Act
            var result = _matchController.GetById(matchId);

            // Assert
            Assert.IsNotNull(result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
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
            Assert.IsNotNull(result);
            var notFoundResult = result.Result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
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
            Assert.IsNotNull(result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(matches, okResult.Value);
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
            Assert.IsNotNull(result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(matches, okResult.Value);
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
            Assert.IsNotNull(result, "Result is null");
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult, "Result is not BadRequestObjectResult");
            Assert.AreEqual(400, badRequestResult.StatusCode, "Status code is not 400");
            Assert.AreEqual("something went wrong", badRequestResult.Value, "Error message does not match");
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
            Assert.IsNotNull(result, "Result is null");
            var okObjectResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okObjectResult, "Result is not OkObjectResult");
            Assert.AreEqual(200, okObjectResult.StatusCode, "Status code is not 200");
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
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddHours(1)
            };
            _matchServiceMock.Setup(service => service.PostToDb(request));

            // Act
            var result = _matchController.Post(request);

            // Assert
            Assert.IsNotNull(result, "Result is null");
            var okObjectResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okObjectResult, "Result is not OkObjectResult");
            Assert.AreEqual(200, okObjectResult.StatusCode, "Status code is not 200");
            Assert.AreEqual("success added new match", okObjectResult.Value, "Returned message is not as expected");
        }


        [Test]
        public void Update_ReturnsOk_OnSuccessfulUpdate()
        {
            // Arrange
            var matchId = 1;
            var existingMatch = new Match { Id = matchId};
            _matchServiceMock.Setup(service => service.GetById(matchId)).Returns(existingMatch);

            // Act
            var result = _matchController.Update(matchId);

            // Assert
            Assert.IsNotNull(result, "Result is null");
            var okObjectResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okObjectResult, "Result is not OkObjectResult");
            Assert.AreEqual(200, okObjectResult.StatusCode, "Status code is not 200");
            Assert.AreEqual("successful update", okObjectResult.Value, "Returned message is not as expected");
        }

        [Test]
        public void Delete_ReturnsOk_OnSuccessfulDelete()
        {
            // Arrange
            var matchId = 1;
            var existingMatch = new Match { Id = matchId};
            _matchServiceMock.Setup(service => service.GetById(matchId)).Returns(existingMatch);

            // Act
            var result = _matchController.Delete(matchId);

            // Assert
            Assert.IsNotNull(result, "Result is null");
            var okObjectResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okObjectResult, "Result is not OkObjectResult");
            Assert.AreEqual(200, okObjectResult.StatusCode, "Status code is not 200");
            Assert.AreEqual("successful delete", okObjectResult.Value, "Returned message is not as expected");
        }

        
        


    }
}
