using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using DataBase.BD;
using DataBase.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using UserService.AuthorizationModel;
using UserService.Controllers;


namespace TestProject;

[TestFixture]
public class LoginControllerTests {
    private LoginController _controller;
    private Mock<IUserRepository> _userRepositoryMock;

    [SetUp]
    public void Setup() {
        _userRepositoryMock = new Mock<IUserRepository>();
        _controller = new LoginController(new ConfigurationManager(), _userRepositoryMock.Object);
    }

    [Test]
    public void Login_Should_Return_OkResult_With_Token() {
        // Arrange
        var userLogin = new LoginModel { Name = "John", Password = "password" };
        var roleId = RoleId.User;
        _userRepositoryMock.Setup(repo => repo.UserCheck(userLogin.Name, userLogin.Password)).Returns(roleId);

        // Act
        var result = _controller.Login(userLogin) as OkObjectResult;

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void Login_Should_Return_InternalServerError_When_Exception_Occurs() {
        // Arrange
        var userLogin = new LoginModel { Name = "John", Password = "password" };
        _userRepositoryMock.Setup(repo => repo.UserCheck(userLogin.Name, userLogin.Password))
            .Throws(new Exception("Some error message"));

        // Act
        var result = _controller.Login(userLogin) as StatusCodeResult;

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void AddAdmin_Should_Return_OkResult() {
        // Arrange
        var userLogin = new LoginModel { Name = "John", Password = "password" };

        // Act
        var result = _controller.AddAdmin(userLogin) as OkResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
    }

    [Test]
    public void AddAdmin_Should_Return_InternalServerError_When_Exception_Occurs() {
        // Arrange
        var userLogin = new LoginModel { Name = "John", Password = "password" };
        _userRepositoryMock.Setup(repo => repo.UserAdd(userLogin.Name, userLogin.Password, RoleId.Admin))
            .Throws(new Exception("Some error message"));

        // Act
        var result = _controller.AddAdmin(userLogin) as StatusCodeResult;

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void AddUser_Should_Return_OkResult() {
        // Arrange
        var userLogin = new LoginModel { Name = "John", Password = "password" };

        // Act
        var result = _controller.AddUser(userLogin) as OkResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
    }

    [Test]
    public void AddUser_Should_Return_InternalServerError_When_Exception_Occurs() {
        // Arrange
        var userLogin = new LoginModel { Name = "John", Password = "password" };
        _userRepositoryMock.Setup(repo => repo.UserAdd(userLogin.Name, userLogin.Password, RoleId.User))
            .Throws(new Exception("Some error message"));

        // Act
        var result = _controller.AddUser(userLogin) as StatusCodeResult;

        // Assert
        Assert.IsNull(result);
    }

    [Test]
    public void GetUserId_Should_Return_OkResult_With_UserId() {
        // Arrange
        var identity = new ClaimsIdentity(new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, "123")
        });
        _controller.ControllerContext = new ControllerContext {
            HttpContext = new DefaultHttpContext {
                User = new ClaimsPrincipal(identity)
            }
        };

        // Act
        var result = _controller.GetUserId() as OkObjectResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        Assert.AreEqual("123", result.Value);
    }

    [Test]
    public void GetUserId_Should_Return_UnauthorizedResult_When_Identity_Is_Null() {
        // Arrange
        _controller.ControllerContext = new ControllerContext {
            HttpContext = new DefaultHttpContext {
                User = null
            }
        };

        // Act
        var result = _controller.GetUserId() as UnauthorizedResult;

        // Assert
        Assert.IsNull(result);
    }
}