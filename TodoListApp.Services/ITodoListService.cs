namespace TodoListApp.Services;
public interface ITodoListService
{
    public IEnumerable<TodoList> GetAll();

    public TodoList GetById(long id);

    public void Create(TodoList todoList);

    public void Update(TodoList todoList);

    public void Delete(long id);

    public bool IsUserEditor(string userName);

}
