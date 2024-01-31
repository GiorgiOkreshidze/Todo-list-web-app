namespace TodoListApp.WebApi.Models;
public class CreateTodoTaskDto
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime DueDate { get; set; }

    public long ListId { get; set; }
}
