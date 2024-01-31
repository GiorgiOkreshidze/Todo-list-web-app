namespace TodoListApp.WebApi.Models;
public class TodoTaskFullDetailsDto
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime DueDate { get; set; }

    public bool Status { get; set; }

    public string ListName { get; set; } = string.Empty;
}
