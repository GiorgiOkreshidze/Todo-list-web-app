namespace TodoListApp.WebApi.Models;
public class TodoTaskDto
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime DueDate { get; set; }

    public bool Status { get; set; }
}
