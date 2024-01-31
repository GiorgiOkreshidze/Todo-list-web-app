namespace TodoListApp.WebApi.Models;
public class UpdateTodoListDto
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}
