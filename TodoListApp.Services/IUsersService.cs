namespace TodoListApp.Services;
public interface IUsersService
{
    bool CheckPassword(User user);
    bool CheckUserName(User user);
    public void Register(User user);
}
