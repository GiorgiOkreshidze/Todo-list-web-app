using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services;
using TodoListApp.WebApi.Models;

namespace TodoListApp.WebApi.Controllers;

#pragma warning disable CS8604
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.

[ApiController]
[Authorize]
[Route("api/[controller]/[action]")]
public class TodoTaskController : Controller
{
    private readonly ITodoTaskService todoTaskService;
    private readonly ITodoListService todoListService;

    public TodoTaskController(ITodoTaskService todoTaskService, ITodoListService todoListService)
    {
        this.todoTaskService = todoTaskService;
        this.todoListService = todoListService;
    }

    [HttpGet]
    public IActionResult GetTodoTasks()
    {
        var todoTasks = this.todoTaskService.GetAll();

        List<TodoTaskDto> todoTaskDto = new List<TodoTaskDto>();

        foreach (var item in todoTasks)
        {
            var todo = new TodoTaskDto()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                DueDate = item.DueDate,
                Status = item.Status,
            };

            todoTaskDto.Add(todo);
        }

        return this.Ok(todoTaskDto);
    }

    [HttpPost]
    public IActionResult CreateTodoTask(CreateTodoTaskDto data)
    {
        string userName = this.User.Identity.Name;

        if (this.todoListService.IsUserEditor(userName))
        {
            var todoTask = new TodoTask()
            {
                Name = data.Name,
                Description = data.Description,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                DueDate = data.DueDate,
                ListId = data.ListId,
                UserName = userName
            };

            this.todoTaskService.Create(todoTask);
        }
        else
        {
            return this.Forbid("User is not editor on given list");
        }
        return this.Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTodoTask(long id)
    {
        string userName = this.User.Identity.Name;

        if (this.todoListService.IsUserEditor(userName))
        {
            var todoTask = this.todoTaskService.GetById(id);

            if (todoTask.Id == 0)
            {
                return this.BadRequest("Todo Task Does Not Exist");
            }
            this.todoTaskService.Delete(id);
        }
        else
        {
            return this.Forbid("User is not editor on given list");
        }
        return this.Ok();
    }

    [HttpPut]
    public IActionResult UpdateTodoTask(UpdateTodoTaskDto data)
    {
        string userName = this.User.Identity.Name;

        if (this.todoListService.IsUserEditor(userName))
        {
            var checkTodoTask = this.todoTaskService.GetById(data.Id);

            if (checkTodoTask.Id == 0)
            {
                return this.BadRequest("Todo Task Does Not Exist");
            }
            var todoTask = new TodoTask()
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                UpdatedDate = DateTime.Now,
                DueDate = data.DueDate,
                Status = data.Status
            };

            this.todoTaskService.Update(todoTask);
        }
        else
        {
            return this.Forbid("User is not editor on given list");
        }
        return this.Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetTodoTaskById(long id)
    {
        var todoTask = this.todoTaskService.GetById(id);

        if (todoTask.Id == 0)
        {
            return this.BadRequest("Todo Task Does Not Exist");
        }

        var todoTaskDto = new UpdateTodoTaskDto()
        {
            Id = todoTask.Id,
            Name = todoTask.Name,
            Description = todoTask.Description,
            DueDate = todoTask.DueDate,
            Status = todoTask.Status
        };

        return this.Ok(todoTaskDto);
    }

    [HttpGet("{listId}")]
    public IActionResult GetTodoTasksByListId(long listId)
    {
        var todoList = this.todoListService.GetById(listId);

        if (todoList.Id == 0)
        {
            return this.BadRequest("Todo List Does Not Exist");
        }

        var todoTasks = this.todoTaskService.GetAllTasksByTodoList(listId);

        List<TodoTaskDto> todoTaskDto = new List<TodoTaskDto>();

        foreach (var item in todoTasks)
        {
            var todo = new TodoTaskDto()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                DueDate = item.DueDate,
                Status = item.Status,
            };

            todoTaskDto.Add(todo);
        }

        return this.Ok(todoTaskDto);
    }

    [HttpGet]
    public IActionResult GetTodoTasksAssignedToMe()
    {
        var todoTasks = this.todoTaskService.GetAllTasksByAssignee(this.User.Identity.Name);

        List<TodoTaskFullDetailsDto> todoTaskFullDetailsDto = new List<TodoTaskFullDetailsDto>();

        foreach (var item in todoTasks)
        {
            var todoListName = this.todoTaskService.GetNameOfTodoList(item.ListId);

            var todo = new TodoTaskFullDetailsDto()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                DueDate = item.DueDate,
                ListName = todoListName,
                Status = item.Status
            };

            todoTaskFullDetailsDto.Add(todo);
        }

        return this.Ok(todoTaskFullDetailsDto);
    }

    [HttpPut("{id}")]
    public IActionResult MoveToDone(long id)
    {
        string userName = this.User.Identity.Name;

        if (this.todoListService.IsUserEditor(userName))
        {
            var todoTask = this.todoTaskService.GetById(id);

            if (todoTask.Id == 0)
            {
                return this.BadRequest("Todo Task Does Not Exist");
            }

            this.todoTaskService.UpdateMoveToDone(id);
        }
        else
        {
            return this.Forbid("User is not editor on given list");
        }
        return this.Ok();
    }



    [HttpGet("{taskId}")]
    public IActionResult GetTagsOfTheTask(long taskId)
    {
        var todoTask = this.todoTaskService.GetById(taskId);

        if (todoTask.Id == 0)
        {
            return this.BadRequest("Todo Task Does Not Exist");
        }

        var tagsOfTheTask = this.todoTaskService.GetTagsOfTheTaskDB(taskId);

        List<TagsDto> tagsDto = new List<TagsDto>();

        foreach (var item in tagsOfTheTask)
        {
            var tag = new TagsDto()
            {
                Id = item.Id,
                Name = item.Name,
            };

            tagsDto.Add(tag);
        }

        return this.Ok(tagsDto);
    }

    [HttpGet]
    public IActionResult GetAllTags()
    {
        var tags = this.todoTaskService.GetAllTagsDB();

        List<TagsDto> tagsDto = new List<TagsDto>();

        foreach (var item in tags)
        {
            var todo = new TagsDto()
            {
                Id = item.Id,
                Name = item.Name,
            };

            tagsDto.Add(todo);
        }

        return this.Ok(tagsDto);
    }


    [HttpPost("{taskId}/{tagId}")]
    public IActionResult AddTagToTheTask(long taskId, long tagId)
    {

        var todoTask = this.todoTaskService.GetById(taskId);

        if (todoTask.Id == 0)
        {
            return this.BadRequest("Todo Task Does Not Exist");
        }

        string userName = this.User.Identity.Name;

        if (this.todoListService.IsUserEditor(userName))
        {
            this.todoTaskService.AddTagToTheTaskDB(taskId, tagId);
        }
        else
        {
            return this.Forbid("User is not editor on given list");
        }
        return this.Ok();
    }

    [HttpDelete("{taskId}/{tagId}")]
    public IActionResult RemoveTagFromTheTask(long taskId, long tagId)
    {
        string userName = this.User.Identity.Name;

        if (this.todoListService.IsUserEditor(userName))
        {
            this.todoTaskService.RemoveTagFromTheTaskDB(taskId, tagId);
        }
        else
        {
            return this.Forbid("User is not editor on given list");
        }
        return this.Ok();
    }


    [HttpGet("{tagId}")]
    public IActionResult GetTasksByTag(long tagId)
    {
        var tasksByTag = this.todoTaskService.GetTasksByTagDB(tagId);

        List<TodoTaskDto> tasksDto = new List<TodoTaskDto>();

        foreach (var item in tasksByTag)
        {
            var task = new TodoTaskDto()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                DueDate = item.DueDate,
                Status = item.Status,
            };

            tasksDto.Add(task);
        }

        return this.Ok(tasksDto);
    }

    [HttpGet("{assignedToMe}/{tagId}")]
    public IActionResult GetFilteredTasks(bool assignedToMe, long tagId)
    {
        string userName = this.User.Identity.Name;

        var filteredTasks = this.todoTaskService.GetFilteredTasksDB(assignedToMe, tagId, userName);

        List<TodoTaskDto> tasksDto = new List<TodoTaskDto>();

        foreach (var item in filteredTasks)
        {
            var task = new TodoTaskDto()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                DueDate = item.DueDate,
                Status = item.Status,
            };

            tasksDto.Add(task);
        }

        return this.Ok(tasksDto);
    }
}
