using TodoListApp.WebApi.Models;

namespace TodoListApp.Services.WebApi.Interfaces;

public interface IUsersWebApiService
{
    void SetBearerToken(string token);
    string LoginUser(UserDto user);
    void Register(UserDto user);
    bool ValidateConnection();
}
