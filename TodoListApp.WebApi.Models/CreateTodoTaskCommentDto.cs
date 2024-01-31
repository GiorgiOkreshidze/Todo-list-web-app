namespace TodoListApp.WebApi.Models;
public class CreateTodoTaskCommentDto
{
    public string Description { get; set; } = string.Empty;

    public long TaskId { get; set; }
}
