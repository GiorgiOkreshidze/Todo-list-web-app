namespace TodoListApp.Services;
public class Tag
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<long> TaskIds { get; set; } = new List<long>();

}
