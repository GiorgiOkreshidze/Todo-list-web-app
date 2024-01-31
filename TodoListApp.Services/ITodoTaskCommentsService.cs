namespace TodoListApp.Services;
public interface ITodoTaskCommentsService
{
    public IEnumerable<TodoTaskComment> GetAll();

    public TodoTaskComment GetById(long id);

    public IEnumerable<TodoTaskComment> GetAllTaskCommentsByTodoTask(long taskId);

    public void Create(TodoTaskComment todoTaskComment);

    public void Update(TodoTaskComment todoTaskComment);

    public void Delete(long id);

    
}
