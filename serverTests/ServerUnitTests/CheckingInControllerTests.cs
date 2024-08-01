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
    public class CheckingInControllerTests
    {
        private Mock<ILogger<CheckingInController>> _loggerMock;
        private Mock<ICheckingInService> _checkingInServiceMock;
        private CheckingInController _checkingInController;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<CheckingInController>>();
            _checkingInServiceMock = new Mock<ICheckingInService>();
            _checkingInController = new CheckingInController(_loggerMock.Object, _checkingInServiceMock.Object);
        }

        [Test]
        public void GetAll_Returns_All_CheckingIns()
        {
            // Arrange
            var expectedCheckingIns = new List<CheckingIn>
            {
                new CheckingIn { Id = 1, UserId = 1, TableId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) },
                new CheckingIn { Id = 2, UserId = 2, TableId = 2, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(2) },
            };

            _checkingInServiceMock.Setup(x => x.GetAll()).Returns(expectedCheckingIns);

            // Act
            var result = _checkingInController.GetAll();

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var actualCheckingIns = okResult.Value as IEnumerable<CheckingIn>;
            Assert.NotNull(actualCheckingIns);
            Assert.AreEqual(expectedCheckingIns.Count, actualCheckingIns.Count());
        }

   
        [Test]
        public void GetByUserId_Returns_CheckingIns_For_Valid_UserId()
        {
            // Arrange
            int userId = 1;
            var expectedCheckingIns = new List<CheckingIn>
            {
                new CheckingIn { Id = 1, UserId = userId, TableId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) },
                new CheckingIn { Id = 2, UserId = userId, TableId = 2, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(2) },
            };

            _checkingInServiceMock.Setup(x => x.GetByUserId(userId)).Returns(expectedCheckingIns);

            // Act
            var result = _checkingInController.GetByUserId(userId);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var actualCheckingIns = okResult.Value as IEnumerable<CheckingIn>;
            Assert.NotNull(actualCheckingIns);
            Assert.AreEqual(expectedCheckingIns.Count, actualCheckingIns.Count());
            Assert.IsTrue(actualCheckingIns.All(c => c.UserId == userId));
        }

        [Test]
        public void GetByUserId_Returns_BadRequest_On_Exception()
        {
            // Arrange
            int userId = 1;
            _checkingInServiceMock.Setup(x => x.GetByUserId(userId)).Throws(new NotFoundException($"CheckingIn with Id:1 not exist in the DB")); // ExceptionBase-t dobunk

            // Act
            var result = _checkingInController.GetByUserId(userId);

            // Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("something went wrong", badRequestResult.Value); // Ellenőrizzük, hogy a válasz üzenete megfelelő
        }


        [Test]
        public void Post_Adds_New_CheckingIn_Successfully()
        {
            // Arrange
            var request = new CheckInRequest
            {
                UserId = 1,
                TableId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(1)
            };

            _checkingInServiceMock.Setup(x => x.PostToDb(request)).Verifiable();

            // Act
            var result = _checkingInController.Post(request);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual("success added new checkingIn", okResult.Value);

            _checkingInServiceMock.Verify(x => x.PostToDb(request), Times.Once);
        }

        [Test]
        public void Post_Returns_BadRequest_On_Exception()
        {
            // Arrange
            var request = new CheckInRequest
            {
                UserId = 1,
                TableId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(1)
            };

            _checkingInServiceMock.Setup(x => x.PostToDb(request)).Throws(new Exception("Simulated error"));

            // Act
            var result = _checkingInController.Post(request);

            // Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("something went wrong", badRequestResult.Value);
        }

        [Test]
        public void Update_Returns_NotFound_When_CheckingIn_Does_Not_Exist()
        {
            // Arrange
            int checkingInId = 1;
            _checkingInServiceMock.Setup(x => x.GetById(checkingInId)).Returns((CheckingIn)null);

            // Act
            var result = _checkingInController.Update(checkingInId, new CheckInRequest());

            // Assert
            var notFoundResult = result.Result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual($"checkingIn with id:{checkingInId} not exist in DB", notFoundResult.Value);
        }

        [Test]
        public void Update_Returns_Successful_When_CheckingIn_Exists()
        {
            // Arrange
            int checkingInId = 1;
            var checkingIn = new CheckingIn { Id = checkingInId, UserId = 1, TableId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(1) };
            var request = new CheckInRequest { UserId = 1, TableId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(1) };

            _checkingInServiceMock.Setup(x => x.GetById(checkingInId)).Returns(checkingIn);
            _checkingInServiceMock.Setup(x => x.Update(checkingInId, request)).Verifiable();

            // Act
            var result = _checkingInController.Update(checkingInId, request);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual("successful update", okResult.Value);
            _checkingInServiceMock.Verify(x => x.Update(checkingInId, request), Times.Once);
        }

        [Test]
        public void Update_Returns_BadRequest_On_Exception()
        {
            // Arrange
            int checkingInId = 1;
            var checkingIn = new CheckingIn { Id = checkingInId, UserId = 1, TableId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(1) };
            var request = new CheckInRequest { UserId = 1, TableId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(1) };

            _checkingInServiceMock.Setup(x => x.GetById(checkingInId)).Returns(checkingIn);
            _checkingInServiceMock.Setup(x => x.Update(checkingInId, request)).Throws(new Exception("Simulated error"));

            // Act
            var result = _checkingInController.Update(checkingInId, request);

            // Assert
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("something went wrong", badRequestResult.Value);
        }

        [Test]
        public void Delete_Returns_NotFound_When_CheckingIn_Does_Not_Exist()
        {
            // Arrange
            int checkingInId = 1;
            _checkingInServiceMock.Setup(x => x.GetById(checkingInId)).Returns((CheckingIn)null);

            // Act
            var result = _checkingInController.Delete(checkingInId);

            // Assert
            var notFoundResult = result.Result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual($"checkingIn with id:{checkingInId} not exist in DB", notFoundResult.Value);
        }

        [Test]
        public void Delete_Returns_Successful_When_CheckingIn_Exists()
        {
            // Arrange
            int checkingInId = 1;
            var checkingIn = new CheckingIn 
            { 
                Id = checkingInId, 
                UserId = 1, 
                TableId = 1, 
                StartDate = DateTime.Now, 
                EndDate = DateTime.Now.AddHours(1) 
            };

            
            _checkingInServiceMock.Setup(x => x.GetById(checkingInId)).Returns(checkingIn);
          
            _checkingInServiceMock.Setup(x => x.DeleteFromDb(checkingInId)).Verifiable();

            // Act
            var result = _checkingInController.Delete(checkingInId);

            // Assert
            var okResult = result.Result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual("successful delete", okResult.Value);
    
            
            _checkingInServiceMock.Verify(x => x.DeleteFromDb(checkingInId), Times.Once);
        }


      



    }
}