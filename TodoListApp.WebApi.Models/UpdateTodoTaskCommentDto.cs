namespace TodoListApp.WebApi.Models;
public class UpdateTodoTaskCommentDto
{
    public long Id { get; set; }

    public string Description { get; set; } = string.Empty;

}
