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

public class TableControllerTests
{
    private Mock<ITableRepository> _tableRepositoryMock;
    private Mock<ILogger<TableController>> _mockLogger;
    private TableController _tableController;
    
    [SetUp]
    public void SetUp()
    {
        _tableRepositoryMock = new Mock<ITableRepository>();
        _mockLogger = new Mock<ILogger<TableController>>();
        _tableController = new TableController(_mockLogger.Object, _tableRepositoryMock.Object);
    }


    [Test]
    public async Task GetAll_ReturnsListOfTables(){}
    
    [Test]
    public async Task GetById_ReturnsRightTable(){}
    
    [Test]
    public async Task Delete_DeletesRightTable(){}

}