namespace UserService.AuthorizationModel;

public class AuthenticationMock: IUserAuthenticationService {
    public UserModel Authenticate(LoginModel model) {
        if (model.Name == "admin" && model.Password == "password") {
            return new UserModel { Password = model.Password, Username = model.Name, Role = UserRole.Adminstrator };
        }

        if (model.Name == "user" && model.Password == "super") {
            return new UserModel { Password = model.Password, Username = model.Name, Role = UserRole.User };
        }

        return null;
    }
}