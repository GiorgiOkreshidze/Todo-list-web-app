using TodoListApp.WebApi.Models;

namespace TodoListApp.Services.WebApi.Interfaces;

public interface ITodoListWebApiService
{
    public void SetBearerToken(string token);

    public List<TodoListDto> GetTodoLists();

    public void CreateTodoList(TodoListDto data);

    public UpdateTodoListDto GetTodoListByIdForUpdate(long id);

    public TodoListDto GetTodoListByIdForDelete(long id);

    public void UpdateTodoList(UpdateTodoListDto data);

    public void Delete(long id);
}
