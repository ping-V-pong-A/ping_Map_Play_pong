using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ping_Map_Play_pong.Controllers;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.ResponseModels;
using ping_Map_Play_pong.Service;
using System;
using System.Collections.Generic;
using ping_Map_Play_pong.Model.Enums;
using ping_Map_Play_pong.Model.RequestModels;

namespace ServerUnitTests
{
    public class UserControllerTests
    {
        private Mock<ILogger<UserController>> _loggerMock;
        private Mock<IUserService> _userServiceMock;
        private UserController _userController;

        [SetUp]
        public void SetUp()
        {
            _loggerMock = new Mock<ILogger<UserController>>();
            _userServiceMock = new Mock<IUserService>();
            _userController = new UserController(_loggerMock.Object, _userServiceMock.Object);
        }
        
        [Test]
        public void GetAll_Returns_Users()
        {
            // Arrange
            var usersResponse = new List<UserResponse>
            {
                new UserResponse { Id = 1, RegistrationDate = DateTime.Now, Rank = Rank.Beginner },
                new UserResponse { Id = 2, RegistrationDate = DateTime.Now, Rank = Rank.Intermediate }
            };
            _userServiceMock.Setup(m => m.GetAll()).Returns(usersResponse);

            // Act
            var result = _userController.GetAll();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var returnedUsers = okResult.Value as List<UserResponse>;
            Assert.IsNotNull(returnedUsers);
            Assert.AreEqual(usersResponse.Count, returnedUsers.Count);
        }

        [Test]
        public void GetAll_Returns_BadRequest_On_Exception()
        {
            // Arrange
            _userServiceMock.Setup(m => m.GetAll()).Throws(new Exception("Test exception"));

            // Act
            var result = _userController.GetAll();

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }
        
        [Test]
        public void GetById_Returns_User()
        {
            // Arrange
            int userId = 1;
            var userResponse = new UserResponse { Id = userId, RegistrationDate = DateTime.Now, Rank = Rank.Beginner };
            _userServiceMock.Setup(m => m.GetById(userId)).Returns(userResponse);

            // Act
            var result = _userController.GetById(userId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var returnedUser = okResult.Value as UserResponse;
            Assert.IsNotNull(returnedUser);
            Assert.AreEqual(userId, returnedUser.Id);
        }

        [Test]
        public void GetById_Returns_BadRequest_On_Exception()
        {
            // Arrange
            int userId = 1;
            _userServiceMock.Setup(m => m.GetById(userId)).Throws(new Exception("Test exception"));

            // Act
            var result = _userController.GetById(userId);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
        }

        [Test]
        public void Update_Returns_Ok_OnSuccessfulUpdate()
        {
            // Arrange
            int userId = 1;
            var userRequest = new UserRequest { /* Initialize with appropriate properties */ };
            _userServiceMock.Setup(m => m.Update(userId, userRequest)).Verifiable();

            // Act
            var result = _userController.Update(userId, userRequest);

            // Assert
            Assert.IsInstanceOf<ActionResult<string>>(result);
            var actionResult = result as ActionResult<string>;
            Assert.IsNotNull(actionResult);

            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual("successful update", okResult.Value);
        }

        [Test]
        public void Update_Returns_BadRequest_On_Exception()
        {
            // Arrange
            int userId = 1;
            var userRequest = new UserRequest { /* Initialize with appropriate properties */ };
            _userServiceMock.Setup(m => m.Update(userId, userRequest)).Throws(new Exception("Test exception"));

            // Act
            var result = _userController.Update(userId, userRequest);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void Delete_Returns_Ok_OnSuccessfulDelete()
        {
            // Arrange
            int userId = 1;
            _userServiceMock.Setup(m => m.Delete(userId)).Verifiable();

            // Act
            var result = _userController.Delete(userId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual("User successfully deleted", okResult.Value);
        }

        [Test]
        public void Delete_Returns_BadRequest_On_Exception()
        {
            // Arrange
            int userId = 1;
            _userServiceMock.Setup(m => m.Delete(userId)).Throws(new Exception("Test exception"));

            // Act
            var result = _userController.Delete(userId);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}