using Microsoft.EntityFrameworkCore;

namespace TodoListApp.Services.Database;
public class TodoTaskDatabaseService : ITodoTaskService
{
    private readonly TodoListDbContext context;

    public TodoTaskDatabaseService(TodoListDbContext context)
    {
        this.context = context;
    }

    public void Create(TodoTask todoTask)
    {
        var todoList = this.context.Set<TodoListEntity>().Where(t => t.Id == todoTask.ListId).First();
        var entity = new TodoTaskEntity
        {
            Name = todoTask.Name,
            Description = todoTask.Description,
            CreatedDate = todoTask.CreatedDate,
            UpdatedDate = todoTask.UpdatedDate,
            DueDate = todoTask.DueDate,
            Status = todoTask.Status,
            TodoListEntity = todoList,
            Assignee = todoTask.UserName
        };

        _ = this.context.Add(entity);
        _ = this.context.SaveChanges();
    }

    public void Delete(long id)
    {
        var entity = new TodoTaskEntity
        {
            Id = id
        };

        this.context.Entry(entity).State = EntityState.Deleted;
        _ = this.context.SaveChanges();
    }

    public IEnumerable<TodoTask> GetAll()
    {
        var list = this.context.Set<TodoTaskEntity>().AsNoTracking().ToList();

        var todoTasks = new List<TodoTask>();

        foreach (var item in list)
        {
            var todo = new TodoTask()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                CreatedDate = item.CreatedDate,
                UpdatedDate = item.UpdatedDate,
                DueDate = item.DueDate,
                Status = item.Status
            };

            todoTasks.Add(todo);
        }

        return todoTasks;
    }

    public IEnumerable<TodoTask> GetAllTasksByTodoList(long listId)
    {
        var list = this.context.Set<TodoTaskEntity>().AsNoTracking().Where(t => t.TodoListEntity.Id == listId).ToList();

        var todoTasks = new List<TodoTask>();

        foreach (var item in list)
        {
            var todo = new TodoTask()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                CreatedDate = item.CreatedDate,
                UpdatedDate = item.UpdatedDate,
                DueDate = item.DueDate,
                Status = item.Status
            };

            todoTasks.Add(todo);
        }

        return todoTasks;
    }

    public string GetNameOfTodoList(long listId)
    {
        var todoList = this.context.Set<TodoListEntity>().Where(t => t.Id == listId).First();

        return todoList.Name;
    }

    public TodoTask GetById(long id)
    {
        var todoTaskData = this.context.Set<TodoTaskEntity>().Where(t => t.Id == id).FirstOrDefault();

        if (todoTaskData == null)
        {
            return new TodoTask();
        }

        var todo = new TodoTask()
        {
            Id = todoTaskData.Id,
            Name = todoTaskData.Name,
            CreatedDate = todoTaskData.CreatedDate,
            UpdatedDate = todoTaskData.UpdatedDate,
            Description = todoTaskData.Description,
            DueDate = todoTaskData.DueDate,
            Status = todoTaskData.Status
        };

        return todo;
    }

    public void Update(TodoTask todoTask)
    {
        var TodoTaskData = this.context.Set<TodoTaskEntity>().Where(t => t.Id == todoTask.Id).First();

        TodoTaskData.Name = todoTask.Name;
        TodoTaskData.Description = todoTask.Description;
        TodoTaskData.UpdatedDate = todoTask.UpdatedDate;
        TodoTaskData.DueDate = todoTask.DueDate;
        TodoTaskData.Status = todoTask.Status;

        _ = this.context.SaveChanges();
    }

    public void UpdateMoveToDone(long Id)
    {
        var TodoTask = this.context.Set<TodoTaskEntity>().Where(t => t.Id == Id).First();

        TodoTask.Status = true;

        _ = this.context.SaveChanges();
    }
    public IEnumerable<TodoTask> GetAllTasksByAssignee(string userName)
    {
        var list = this.context.Set<TodoTaskEntity>().Where(u => u.Assignee == userName).AsNoTracking().ToList();


        var todoTasks = new List<TodoTask>();

        foreach (var item in list)
        {
            var todo = new TodoTask()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                CreatedDate = item.CreatedDate,
                UpdatedDate = item.UpdatedDate,
                DueDate = item.DueDate,
                Status = item.Status,
                ListId = item.TodoListEntity.Id
            };

            todoTasks.Add(todo);
        }

        return todoTasks;
    }



    public IEnumerable<Tag> GetTagsOfTheTaskDB(long taskId)
    {
        var TagEntities = this.context.Set<TagEntity>().Where(t => t.todoTaskEntities.Any(z => z.Id == taskId)).ToList();

        var Tags = new List<Tag>();

        foreach (var item in TagEntities)
        {
            var Tag = new Tag()
            {
                Id = item.Id,
                Name = item.Name,
            };

            Tags.Add(Tag);
        }

        return Tags;
    }

    public IEnumerable<Tag> GetAllTagsDB()
    {
        var TagEntities = this.context.Set<TagEntity>().AsNoTracking().ToList();

        var Tags = new List<Tag>();

        foreach (var item in TagEntities)
        {
            var Tag = new Tag()
            {
                Id = item.Id,
                Name = item.Name,
            };

            Tags.Add(Tag);
        }

        return Tags;
    }

    public void AddTagToTheTaskDB(long taskId, long tagId)
    {
        var TagEntity = this.context.Set<TagEntity>().Where(t => t.Id == tagId).First();

        var TodoTaskEntity = this.context.Set<TodoTaskEntity>().Include(a => a.TagEntities).Where(t => t.Id == taskId).First();

        TodoTaskEntity.TagEntities.Add(TagEntity);

        _ = this.context.SaveChanges();
    }

    public void RemoveTagFromTheTaskDB(long taskId, long tagId)
    {
        var TagEntity = this.context.Set<TagEntity>().Where(t => t.Id == tagId).First();

        var TodoTaskEntity = this.context.Set<TodoTaskEntity>().Include(a => a.TagEntities).Where(t => t.Id == taskId).First();

        _ = TodoTaskEntity.TagEntities.Remove(TagEntity);

        _ = this.context.SaveChanges();
    }

    public IEnumerable<TodoTask> GetTasksByTagDB(long tagId)
    {
        var TodoTaskEntities = this.context.Set<TodoTaskEntity>().Where(t => t.TagEntities.Any(z => z.Id == tagId)).ToList();

        var Tasks = new List<TodoTask>();

        foreach (var item in TodoTaskEntities)
        {
            var Task = new TodoTask()
            {
                Id = item.Id,
                Name = item.Name,
            };

            Tasks.Add(Task);
        }

        return Tasks;
    }

    public IEnumerable<TodoTask> GetFilteredTasksDB(bool assignedToMe, long tagId, string userName)
    {

        var TodoTaskQuery = this.context.Set<TodoTaskEntity>().AsQueryable();

        if (assignedToMe)
        {
            TodoTaskQuery = TodoTaskQuery.Where(t => t.Assignee == userName);
        }

        if (tagId != 0)
        {
            TodoTaskQuery = TodoTaskQuery.Where(t => t.TagEntities.Any(z => z.Id == tagId));
        }

        var TodoTaskEntities = TodoTaskQuery.ToList();

        var Tasks = new List<TodoTask>();

        foreach (var item in TodoTaskEntities)
        {
            var Task = new TodoTask()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                CreatedDate = item.CreatedDate,
                UpdatedDate = item.UpdatedDate,
                DueDate = item.DueDate,
                Status = item.Status,
                ListId = item.TodoListEntity.Id,
            };

            Tasks.Add(Task);
        }

        return Tasks;
    }
}
