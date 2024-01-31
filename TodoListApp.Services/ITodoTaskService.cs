namespace TodoListApp.Services;
public interface ITodoTaskService
{
    IEnumerable<TodoTask> GetTasksByTagDB(long tagId);

    IEnumerable<TodoTask> GetFilteredTasksDB(bool assignedToMe, long tagId, string userName);

    public IEnumerable<TodoTask> GetAllTasksByTodoList(long listId);

    public IEnumerable<TodoTask> GetAllTasksByAssignee(string userName);

    public IEnumerable<TodoTask> GetAll();

    public TodoTask GetById(long id);

    public string GetNameOfTodoList(long listId);

    IEnumerable<Tag> GetTagsOfTheTaskDB(long taskId);

    IEnumerable<Tag> GetAllTagsDB();

    public void Create(TodoTask todoTask);

    public void Update(TodoTask todoTask);

    public void Delete(long id);

    public void UpdateMoveToDone(long Id);

    void AddTagToTheTaskDB(long taskId, long tagId);

    void RemoveTagFromTheTaskDB(long taskId, long tagId);

}
