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
public class TodoTaskCommentController : Controller
{
    private readonly ITodoTaskCommentsService todoTaskCommentsService;
    private readonly ITodoListService todoListService;

    public TodoTaskCommentController(ITodoTaskCommentsService todoTaskCommentsService, ITodoListService todoListService)
    {
        this.todoTaskCommentsService = todoTaskCommentsService;
        this.todoListService = todoListService;
    }

    [HttpGet]
    public IActionResult GetTodoTaskComments()
    {
        var todoTaskComments = this.todoTaskCommentsService.GetAll();

        List<TodoTaskCommentDto> todoTaskCommentDto = new List<TodoTaskCommentDto>();

        foreach (var item in todoTaskComments)
        {
            var todo = new TodoTaskCommentDto()
            {
                Id = item.Id,
                Description = item.Description
            };

            todoTaskCommentDto.Add(todo);
        }

        return this.Ok(todoTaskCommentDto);

    }

    [HttpPost]
    public IActionResult CreateTodoTaskComment(CreateTodoTaskCommentDto data)
    {
        string userName = this.User.Identity.Name;

        if (this.todoListService.IsUserEditor(userName))
        {
            var todoTaskComment = new TodoTaskComment()
            {
                Description = data.Description,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                TaskId = data.TaskId
            };

            this.todoTaskCommentsService.Create(todoTaskComment);
        }
        else
        {
            return this.Forbid("User is not editor on given list");
        }
        return this.Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTodoTaskComment(long id)
    {
        string userName = this.User.Identity.Name;

        if (this.todoListService.IsUserEditor(userName))
        {
            this.todoTaskCommentsService.Delete(id);
        }
        else
        {
            return this.Forbid("User is not editor on given list");
        }
        return this.Ok();
    }

    [HttpPut]
    public IActionResult UpdateTodoTaskComment(UpdateTodoTaskCommentDto data)
    {
        string userName = this.User.Identity.Name;

        if (this.todoListService.IsUserEditor(userName))
        {
            var todoTaskComment = new TodoTaskComment()
            {
                Id = data.Id,
                Description = data.Description,
                Updated = DateTime.Now
            };
            this.todoTaskCommentsService.Update(todoTaskComment);
        }
        else
        {
            return this.Forbid("User is not editor on given list");
        }
        return this.Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetTodoTaskCommentById(long id)
    {
        var todoTaskComment = this.todoTaskCommentsService.GetById(id);

        var todoTaskCommentDto = new UpdateTodoTaskCommentDto()
        {
            Id = todoTaskComment.Id,
            Description = todoTaskComment.Description
        };

        return this.Ok(todoTaskCommentDto);
    }

    [HttpGet("{taskId}")]
    public IActionResult GetTodoTaskCommentsByTaskId(long taskId)
    {
        var todoTaskComments = this.todoTaskCommentsService.GetAllTaskCommentsByTodoTask(taskId);

        List<TodoTaskCommentDto> todoTaskCommentDto = new List<TodoTaskCommentDto>();

        foreach (var item in todoTaskComments)
        {
            var todo = new TodoTaskCommentDto()
            {
                Id = item.Id,
                Description = item.Description
            };

            todoTaskCommentDto.Add(todo);
        }

        return this.Ok(todoTaskCommentDto);
    }
}
