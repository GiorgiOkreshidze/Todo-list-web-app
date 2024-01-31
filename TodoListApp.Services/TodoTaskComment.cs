namespace TodoListApp.Services;
public class TodoTaskComment
{
    public long Id { get; set; }

    public string Description { get; set; } = string.Empty;

    public DateTime Created { get; set; }

    public DateTime Updated { get; set; }

    public long TaskId { get; set; }
}
