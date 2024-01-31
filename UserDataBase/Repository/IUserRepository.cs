using UserDataBase.BD;

namespace UserDataBase.Repository;

public interface IUserRepository {
    public void UserAdd(string name, string password, RoleId roleId);
    public RoleId UserCheck(string name, string password);
    public List<User> GetAllUsers();
    public void DeleteUser(string name);
}