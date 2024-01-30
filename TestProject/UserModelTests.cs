using UserService.AuthorizationModel;

namespace TestProject;

[TestFixture]
public class UserModelTests {
    private UserModel _userModel;

    [SetUp]
    public void Setup() {
        _userModel = new UserModel();
    }

    [Test]
    public void TestUsernameProperty() {
        string expectedUsername = "TestUser";
        _userModel.Username = expectedUsername;

        Assert.AreEqual(expectedUsername, _userModel.Username);
    }

    [Test]
    public void TestPasswordProperty() {
        string expectedPassword = "TestPassword";
        _userModel.Password = expectedPassword;

        Assert.AreEqual(expectedPassword, _userModel.Password);
    }

    [Test]
    public void TestRoleProperty() {
        UserRole expectedRole = UserRole.Adminstrator;
        _userModel.Role = expectedRole;

        Assert.AreEqual(expectedRole, _userModel.Role);
    }
}