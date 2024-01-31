using Microsoft.EntityFrameworkCore;

namespace TodoListApp.Services.Database;
public class TodoTaskCommentDatabaseService : ITodoTaskCommentsService
{
    private readonly TodoListDbContext context;

    public TodoTaskCommentDatabaseService(TodoListDbContext context)
    {
        this.context = context;
    }

    public void Create(TodoTaskComment todoTaskComment)
    {
        var todoTask = this.context.Set<TodoTaskEntity>().Where(t => t.Id == todoTaskComment.TaskId).First();
        var entity = new TodoTaskCommentEntity
        {
            Description = todoTaskComment.Description,
            Created = todoTaskComment.Created,
            Updated = todoTaskComment.Updated,
            todoTaskEntity = todoTask
        };

        _ = this.context.Add(entity);
        _ = this.context.SaveChanges();
    }

    public void Delete(long id)
    {
        var entity = new TodoTaskCommentEntity
        {
            Id = id
        };

        this.context.Entry(entity).State = EntityState.Deleted;
        _ = this.context.SaveChanges();
    }

    public IEnumerable<TodoTaskComment> GetAll()
    {
        var listOfCommentEntities = this.context.Set<TodoTaskCommentEntity>().AsNoTracking().ToList();

        var todoTaskComments = new List<TodoTaskComment>();

        foreach (var item in listOfCommentEntities)
        {
            var todo = new TodoTaskComment()
            {
                Id = item.Id,
                Description = item.Description,
                Created = item.Created,
                Updated = item.Updated,
            };

            todoTaskComments.Add(todo);
        }

        return todoTaskComments;
    }

    public void Update(TodoTaskComment todoTaskComment)
    {
        var TodoTaskCommentData = this.context.Set<TodoTaskCommentEntity>().Where(t => t.Id == todoTaskComment.Id).First();

        TodoTaskCommentData.Description = todoTaskComment.Description;
        TodoTaskCommentData.Updated = todoTaskComment.Updated;

        _ = this.context.SaveChanges();
    }

    public TodoTaskComment GetById(long id)
    {
        var todoTaskCommentData = this.context.Set<TodoTaskCommentEntity>().Where(t => t.Id == id).First();

        var todo = new TodoTaskComment()
        {
            Id = todoTaskCommentData.Id,
            Description = todoTaskCommentData.Description,
            Created = todoTaskCommentData.Created,
            Updated = todoTaskCommentData.Updated
        };

        return todo;
    }

    public IEnumerable<TodoTaskComment> GetAllTaskCommentsByTodoTask(long taskId)
    {
        var list = this.context.Set<TodoTaskCommentEntity>().AsNoTracking().Where(t => t.todoTaskEntity.Id == taskId).ToList();

        var todoTaskComments = new List<TodoTaskComment>();

        foreach (var item in list)
        {
            var todo = new TodoTaskComment()
            {
                Id = item.Id,
                Description = item.Description,
                Created = item.Created,
                Updated = item.Updated
            };

            todoTaskComments.Add(todo);
        }

        return todoTaskComments;
    }
}
