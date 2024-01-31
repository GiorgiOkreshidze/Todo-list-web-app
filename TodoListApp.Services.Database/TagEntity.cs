namespace TodoListApp.Services.Database;
public class TagEntity
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<TodoTaskEntity> todoTaskEntities { get; set; } = new List<TodoTaskEntity>();
}
