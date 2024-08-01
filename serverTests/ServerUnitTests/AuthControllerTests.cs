using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using ping_Map_Play_pong.Contracts;
using ping_Map_Play_pong.Controllers;
using ping_Map_Play_pong.Service.Authentication;
using ping_Map_Play_pong.Service;

namespace ServerUnitTests
{
    public class AuthControllerTests
    {
        private Mock<IAuthService> _authServiceMock;
        private Mock<IUserService> _userServiceMock;
        private Mock<IConfiguration> _configurationMock;
        private AuthController _authController;

        [SetUp]
        public void SetUp()
        {
            _authServiceMock = new Mock<IAuthService>();
            _userServiceMock = new Mock<IUserService>();
            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(c => c["SomeConfigKey"]).Returns("SomeConfigValue");

            _authController = new AuthController(_authServiceMock.Object, _configurationMock.Object, _userServiceMock.Object); // Changed to _userServiceMock
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
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);

            var errors = badRequestResult.Value as SerializableError;
            Assert.NotNull(errors);
            Assert.IsTrue(errors.ContainsKey("Email"));
            Assert.IsTrue(errors.ContainsKey("Username"));
            Assert.IsTrue(errors.ContainsKey("Password"));
        }

        [Test]
        public async Task Register_ValidModel_ReturnsCreatedResponse()
        {
            // Arrange
            var validRequest = new RegistrationRequest("email@email.hu", "user1", "password123");
            var expectedResponse = new AuthResult(true, "email@email.hu", "user1", "");

            
            _configurationMock.Setup(c => c.GetSection("AppRoles:UserRoleName").Value).Returns("User");

         
            _authServiceMock.Setup(x => x.RegisterAsync(validRequest.Email, validRequest.Username, validRequest.Password, "User"))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _authController.Register(validRequest);

            // Assert
            var createdResult = result.Result as CreatedAtActionResult;
            Assert.NotNull(createdResult);
            Assert.AreEqual(201, createdResult.StatusCode);
            Assert.IsInstanceOf<RegistrationResponse>(createdResult.Value);

            var registrationResponse = createdResult.Value as RegistrationResponse;
            Assert.AreEqual(expectedResponse.Email, registrationResponse.Email);
            Assert.AreEqual(expectedResponse.UserName, registrationResponse.Username);
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
            var badRequestResult = result.Result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);

            var errors = badRequestResult.Value as SerializableError;
            Assert.NotNull(errors);
            Assert.IsTrue(errors.ContainsKey("Email"));
        }
    }
}