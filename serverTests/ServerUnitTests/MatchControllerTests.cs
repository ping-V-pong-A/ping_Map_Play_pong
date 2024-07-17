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

public class MatchControllerTests
{
    private Mock<ILogger<MatchController>> _loggerMock;
    private Mock<IMatchRepository> _matchRepositoryMock;
    private Mock<IUserRepository> _userRepositoryMock;
    private Mock<ITableRepository> _tableRepositoryMock;
    private MatchController _matchController;

    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger<MatchController>>();
        _matchRepositoryMock = new Mock<IMatchRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _tableRepositoryMock = new Mock<ITableRepository>();

        _matchController = new MatchController(_loggerMock.Object, 
            _matchRepositoryMock.Object, 
            _userRepositoryMock.Object, 
            _tableRepositoryMock.Object);
    }

    [Test]
    public void GetById_ReturnsRightMatch()
    {
    }
    
    [Test]
    public void GetByPlayerIds_Return_RightMatchByOnePlayerId(){}
    
    [Test]
    public void GetByPlayerIds_Return_RightMatchByBothPlayerIds(){}
}