namespace UserService.AuthorizationModel;

public interface IUserAuthenticationService {
    UserModel Authenticate(LoginModel model);
}