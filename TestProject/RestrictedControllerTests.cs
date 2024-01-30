using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using DataBase.BD;
using UserService.Controllers;
using UserService.AuthorizationModel;
using DataBase.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;


namespace TestProject {
    [TestFixture]
    public class RestrictedControllerTests {
        private RestrictedController _controller;
        private Mock<IUserRepository> _userRepositoryMock;

        [SetUp]
        public void Setup() {
            _userRepositoryMock = new Mock<IUserRepository>();
            _controller = new RestrictedController(_userRepositoryMock.Object);
        }

        [Test]
        public void AdminEndPoint_Should_Return_OkResult_With_Correct_Role() {
            // Arrange
            var currentUser = new UserModel { Role = UserRole.Adminstrator };
            _controller.ControllerContext = new ControllerContext {
                HttpContext = new DefaultHttpContext {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Role, UserRole.Adminstrator.ToString())
                    }))
                }
            };

            // Act
            var result = _controller.AdminEndPoint() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual($"Hi you are an {currentUser.Role}", result.Value);
        }

        [Test]
        public void UserEndPoint_Should_Return_OkResult_With_Correct_Role() {
            // Arrange
            var currentUser = new UserModel { Role = UserRole.User };
            _controller.ControllerContext = new ControllerContext {
                HttpContext = new DefaultHttpContext {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Role, UserRole.User.ToString())
                    }))
                }
            };

            // Act
            var result = _controller.UserEndPoint() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual($"Hi you are an {currentUser.Role}", result.Value);
        }

        [Test]
        public void GetAllUsers_Should_Return_OkResult_With_All_Users() {
            // Arrange
            var allUsers = new List<User>();
            _userRepositoryMock.Setup(repo => repo.GetAllUsers()).Returns(allUsers);

            // Act
            var result = _controller.GetAllUsers();

            // Assert
            Assert.IsNotNull(result);
        }
    }
}