namespace TodoListApp.WebApi.Models;
public class TodoListDto
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
}
