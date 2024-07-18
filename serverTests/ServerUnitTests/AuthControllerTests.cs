
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using ping_Map_Play_pong.Contracts;
using ping_Map_Play_pong.Controllers;
using ping_Map_Play_pong.Service.Authentication;
using ping_Map_Play_pong.Service.Repositories;

namespace ServerUnitTests
{
    public class AuthControllerTests
    {
        private Mock<IAuthService> _authServiceMock;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IConfiguration> _configurationMock;
        private AuthController _authController;


        [SetUp]
        public void SetUp()
        {
            _authServiceMock = new Mock<IAuthService>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(c => c["SomeConfigKey"]).Returns("SomeConfigValue");

            _authController = new AuthController(_authServiceMock.Object, _configurationMock.Object,
                _userRepositoryMock.Object);
        }

        [Test]
        public async Task Register_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var invalidRequest = new RegistrationRequest("", "", "");

            _authController.ModelState.AddModelError("Email", "The Email field is required.");
            _authController.ModelState.AddModelError("Username", "The Username field is required.");
            _authController.ModelState.AddModelError("Password", "The Password field is required.");

            // Act
            var result = await _authController.Register(invalidRequest);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);

            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.AreEqual(400, badRequestResult.StatusCode);

            var errors = badRequestResult.Value as SerializableError;
            Assert.NotNull(errors);

            var errorKeys = new List<string>(errors.Keys);
            Assert.IsTrue(errorKeys.Contains("Email"));
            Assert.IsTrue(errorKeys.Contains("Username"));
            Assert.IsTrue(errorKeys.Contains("Password"));
        }

        [Test]
        public async Task Register_ValidModel_ReturnsRegistrationResponse()
        {
            // Arrange
            var invalidRequest = new RegistrationRequest("email@email.hu", "user1", "password123");

            _authController.ModelState.AddModelError("Email", "The Email field is required.");
            _authController.ModelState.AddModelError("Username", "The Username field is required.");
            _authController.ModelState.AddModelError("Password", "The Password field is required.");

            // Act
            var result = await _authController.Register(invalidRequest);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);

            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.AreEqual(400, badRequestResult.StatusCode);

            var errors = badRequestResult.Value as SerializableError;
            Assert.NotNull(errors);

            var errorKeys = new List<string>(errors.Keys);
            Assert.IsTrue(errorKeys.Contains("Email"));
            Assert.IsTrue(errorKeys.Contains("Username"));
            Assert.IsTrue(errorKeys.Contains("Password"));
        }

        [Test]
        public async Task Authenticate_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var invalidRequest = new AuthRequest("", "");
            _authController.ModelState.AddModelError("Email", "The Email field is required.");

            // Act
            var result = await _authController.Authenticate(invalidRequest);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);

            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.AreEqual(400, badRequestResult.StatusCode);

            var errors = badRequestResult.Value as SerializableError;
            Assert.NotNull(errors);
            Assert.IsTrue(errors.ContainsKey("Email"));
        }



    }

}

    