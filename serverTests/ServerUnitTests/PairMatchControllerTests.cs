using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ping_Map_Play_pong.Controllers;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.RequestModels;
using ping_Map_Play_pong.Service;
using ping_Map_Play_pong.Model.Exceptions;

namespace ServerUnitTests
{
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
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
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
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
            var notFoundResult = result.Result as NotFoundObjectResult;
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
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
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
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okObjectResult = result.Result as OkObjectResult;
            Assert.AreEqual(200, okObjectResult.StatusCode);
            var returnedMatches = okObjectResult.Value as IEnumerable<PairMatch>;
            Assert.NotNull(returnedMatches);
            Assert.AreEqual(expectedMatches.Count, returnedMatches.Count());
            CollectionAssert.AreEqual(expectedMatches, returnedMatches);
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
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okObjectResult = result.Result as OkObjectResult;
            Assert.AreEqual(200, okObjectResult.StatusCode);
            var returnedMatches = okObjectResult.Value as IEnumerable<PairMatch>;
            Assert.NotNull(returnedMatches);
            Assert.AreEqual(expectedMatches.Count, returnedMatches.Count());
            CollectionAssert.AreEqual(expectedMatches, returnedMatches);
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
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okObjectResult = result.Result as OkObjectResult;
            Assert.AreEqual(200, okObjectResult.StatusCode);
            var returnedMatch = okObjectResult.Value as PairMatch;
            Assert.NotNull(returnedMatch);
            Assert.AreEqual(expectedMatch.Id, returnedMatch.Id);
            Assert.AreEqual(expectedMatch.StartDate, returnedMatch.StartDate);
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
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
            var notFoundResult = result.Result as NotFoundObjectResult;
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual($"pairPairMatch with id:{pairMatchId} not exist in DB", notFoundResult.Value);
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
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddHours(1)
            };
            _pairMatchServiceMock.Setup(service => service.PostToDb(request)).Verifiable();

            // Act
            var result = _pairMatchController.Post(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okObjectResult = result.Result as OkObjectResult;
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.AreEqual("success added new pairPairMatch", okObjectResult.Value);

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
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddHours(1)
            };
            _pairMatchServiceMock.Setup(service => service.PostToDb(request)).Throws(new Exception("Error adding match"));

            // Act
            var result = _pairMatchController.Post(request);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            var badRequestObjectResult = result.Result as BadRequestObjectResult;
            Assert.AreEqual(400, badRequestObjectResult.StatusCode);
            Assert.AreEqual("un success added new pairPairMatch", badRequestObjectResult.Value);

            _pairMatchServiceMock.Verify(service => service.PostToDb(request), Times.Once);
        }

        [Test]
        public void Update_ReturnsOk_OnSuccessfulUpdate()
        {
            // Arrange
            var pairMatchId = 1;
            var existingPairMatch = new PairMatch { Id = pairMatchId };
            var request = new PairMatchRequest { /* fill in with appropriate data */ };

            _pairMatchServiceMock.Setup(service => service.GetById(pairMatchId)).Returns(existingPairMatch);
            _pairMatchServiceMock.Setup(service => service.Update(pairMatchId, request)).Verifiable();

            // Act
            var result = _pairMatchController.Update(pairMatchId, request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okObjectResult = result.Result as OkObjectResult;
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.AreEqual($"successfully updated pairMatch with id: {pairMatchId}", okObjectResult.Value);

            _pairMatchServiceMock.Verify(service => service.Update(pairMatchId, request), Times.Once);
        }

        [Test]
        public void Update_ReturnsNotFound_OnInvalidId()
        {
            // Arrange
            var pairMatchId = 1;
            var request = new PairMatchRequest { /* fill in with appropriate data */ };

            _pairMatchServiceMock.Setup(service => service.GetById(pairMatchId)).Throws(new Exception("Match not found"));

            // Act
            var result = _pairMatchController.Update(pairMatchId, request);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
            var notFoundResult = result.Result as NotFoundObjectResult;
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual($"pairPairMatch with id:{pairMatchId} not exist in DB", notFoundResult.Value);
        }



        [Test]
        public void Delete_ReturnsOk_OnSuccessfulDeletion()
        {
            // Arrange
            var pairMatchId = 1;
            var existingPairMatch = new PairMatch { Id = pairMatchId };
            _pairMatchServiceMock.Setup(service => service.GetById(pairMatchId)).Returns(existingPairMatch);
            _pairMatchServiceMock.Setup(service => service.DeleteFromDb(pairMatchId)).Verifiable();

            // Act
            var result = _pairMatchController.Delete(pairMatchId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okObjectResult = result.Result as OkObjectResult;
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.AreEqual($"successful delete", okObjectResult.Value);

          
            _pairMatchServiceMock.Verify(service => service.DeleteFromDb(pairMatchId), Times.Once);
        }

        [Test]
        public void Delete_ReturnsNotFound_OnInvalidId()
        {
            // Arrange
            var pairMatchId = 1;
            _pairMatchServiceMock.Setup(service => service.GetById(pairMatchId)).Returns((PairMatch)null);
            _pairMatchServiceMock.Setup(service => service.DeleteFromDb(pairMatchId)).Throws(new NotFoundException($"match with id:{pairMatchId} not exist in DB"));

            // Act
            var result = _pairMatchController.Delete(pairMatchId);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
            var notFoundResult = result.Result as NotFoundObjectResult;
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual($"match with id:{pairMatchId} not exist in DB", notFoundResult.Value);
        }

    }
}