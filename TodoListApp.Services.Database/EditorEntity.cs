namespace TodoListApp.Services.Database;
public class EditorEntity
{
    public long Id { get; set; }

    public string UserName { get; set; } = string.Empty;

    public TodoListEntity TodoListEntity { get; set; } = new TodoListEntity();
}
