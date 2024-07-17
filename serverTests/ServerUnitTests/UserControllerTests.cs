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

namespace ServerUnitTests;

public class UserControllerTests
{
    private Mock<IUserRepository> _userRepository;
    private Mock<ILogger<UserController>> _mockLogger;
    private Mock<UserManager<IdentityUser>> _userManagerMock;
    private UserController _userController;

    [SetUp]
    public void SetUp()
    {
        _userRepository = new Mock<IUserRepository>();
        _mockLogger = new Mock<ILogger<UserController>>();
        _userManagerMock = new Mock<UserManager<IdentityUser>>();

        _userController = new UserController(_mockLogger.Object, _userRepository.Object, _userManagerMock.Object);
    }
    
    
    [Test]
    public void GetById_ReturnsRightUser()
    {
    }


    [Test]
    public void GetAll_ReturnsOkResult_WithListOfUsers()
    {
    }

    [Test]
    public void Update_ReturnsOkResult_AfterUpdate()
    {
    }

    [Test]
    public async Task Delete_ReturnsOkResult_AfterDelete()
    {
    }
}