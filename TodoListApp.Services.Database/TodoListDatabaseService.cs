using Microsoft.EntityFrameworkCore;

namespace TodoListApp.Services.Database;
public class TodoListDatabaseService : ITodoListService
{
    private readonly TodoListDbContext context;

    public TodoListDatabaseService(TodoListDbContext context)
    {
        this.context = context;
    }

    public void Create(TodoList todoList)
    {
        var entity = new TodoListEntity
        {
            Name = todoList.Name,
            Description = todoList.Description,
            CreatedDate = todoList.CreatedDate,
            UpdatedDate = todoList.UpdatedDate
        };

        _ = this.context.Add(entity);
        _ = this.context.SaveChanges();
    }

    public IEnumerable<TodoList> GetAll()
    {
        var list = this.context.Set<TodoListEntity>().AsNoTracking().ToList();

        var todoList = new List<TodoList>();

        foreach (var item in list)
        {
            var todo = new TodoList()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                CreatedDate = item.CreatedDate,
                UpdatedDate = item.UpdatedDate
            };

            todoList.Add(todo);
        }

        return todoList;
    }

    public TodoList GetById(long id)
    {
        var todoListData = this.context.Set<TodoListEntity>().Where(t => t.Id == id).FirstOrDefault();

        if (todoListData == null)
        {
            return new TodoList();
        }

        var todo = new TodoList()
        {
            Id = todoListData.Id,
            Name = todoListData.Name,
            CreatedDate = todoListData.CreatedDate,
            UpdatedDate = todoListData.UpdatedDate,
            Description = todoListData.Description
        };

        return todo;
    }

    public void Update(TodoList todoList)
    {
        var todoListData = this.context.Set<TodoListEntity>().Where(t => t.Id == todoList.Id).First();

        todoListData.Name = todoList.Name;
        todoListData.Description = todoList.Description;
        todoListData.UpdatedDate = todoList.UpdatedDate;

        _ = this.context.SaveChanges();
    }

    public void Delete(long id)
    {
        var entity = new TodoListEntity
        {
            Id = id
        };

        this.context.Entry(entity).State = EntityState.Deleted;

        _ = this.context.SaveChanges();
    }

    public bool IsUserEditor(string userName)
    {
        return this.context.Set<TodoListEntity>().Any(t => t.Editors.Any(e => e.UserName == userName));
    }
}
