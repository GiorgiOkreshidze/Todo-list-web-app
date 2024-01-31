namespace TodoListApp.WebApi.Models;
public class CreateTodoListDto
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}
