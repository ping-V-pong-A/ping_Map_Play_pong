using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ping_Map_Play_pong.Controllers;
using ping_Map_Play_pong.Model.DataModels;
using ping_Map_Play_pong.Model.ResponseModels;
using ping_Map_Play_pong.Service.Repositories;
using NUnit.Framework;
using ping_Map_Play_pong.Model;
using ping_Map_Play_pong.Model.Enums;
using ping_Map_Play_pong.Service;

namespace ServerUnitTests;

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
        var users = new List<User>
        {
            new User { Id = 1, RegistrationDate = DateTime.Now, CheckedInTables = new List<Table>(), Rank = Rank.Beginner },
            new User { Id = 2, RegistrationDate = DateTime.Now, CheckedInTables = new List<Table>(), Rank = Rank.Intermediate }
        };
        _userServiceMock.Setup(m => m.GetAll()).Returns(users);

        // Act
        var result = _userController.GetAll();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var usersResponse = okResult.Value as List<UserResponse>;
        Assert.IsNotNull(usersResponse);
        Assert.AreEqual(users.Count, usersResponse.Count);
        
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
        var user = new User { Id = userId, RegistrationDate = DateTime.Now, CheckedInTables = new List<Table>(), Rank = Rank.Beginner };
        _userServiceMock.Setup(m => m.GetById(userId)).Returns(user);

        // Act
        var result = _userController.GetById(userId);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);

        var userResponse = okResult.Value as User;
        Assert.IsNotNull(userResponse);
        Assert.AreEqual(userId, userResponse.Id);
     
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
    public void Delete_Returns_Ok_OnSuccessfulDelete()
    {
        // Arrange
        int userId = 1;
        var existingUser = new User { Id = userId, RegistrationDate = DateTime.Now, CheckedInTables = new List<Table>(), Rank = Rank.Beginner };
        _userServiceMock.Setup(m => m.GetById(userId)).Returns(existingUser);

        // Act
        var result = _userController.Delete(userId);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual("User successfully deleted", okResult.Value);
    }

    [Test]
    public void Delete_Returns_NotFound_If_User_Not_Found()
    {
        // Arrange
        int userId = 1;
        _userServiceMock.Setup(m => m.GetById(userId)).Returns((User)null);

        // Act
        var result = _userController.Delete(userId);

        // Assert
        Assert.IsInstanceOf<NotFoundObjectResult>(result);
        var notFoundResult = result as NotFoundObjectResult;
        Assert.IsNotNull(notFoundResult);
        Assert.AreEqual($"User with ID:{userId} not found in database", notFoundResult.Value);
    }
  

}

