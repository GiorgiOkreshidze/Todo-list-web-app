namespace TodoListApp.Services;
public class TodoTask
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public DateTime DueDate { get; set; }

    public long ListId { get; set; }

    public bool Status { get; set; }

    public string UserName { get; set; }
}
