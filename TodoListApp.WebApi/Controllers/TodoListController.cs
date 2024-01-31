using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.Services;
using TodoListApp.WebApi.Models;

namespace TodoListApp.WebApi.Controllers;
[ApiController]
[Authorize]
[Route("api/[controller]/[action]")]
public class TodoListController : Controller
{
    private readonly ITodoListService todoListService;

    public TodoListController(ITodoListService todoListService)
    {
        this.todoListService = todoListService;
    }

    [HttpGet]
    public IActionResult GetTodoLists()
    {
        var todoLists = this.todoListService.GetAll();

        List<TodoListDto> todoListDtos = new List<TodoListDto>();

        foreach (var item in todoLists)
        {
            var todo = new TodoListDto()
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description
            };

            todoListDtos.Add(todo);
        }

        return this.Ok(todoListDtos);
    }

    [HttpPost]
    public IActionResult CreateTodoList(CreateTodoListDto data)
    {
        var todoList = new TodoList()
        {
            Name = data.Name,
            Description = data.Description,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now
        };

        this.todoListService.Create(todoList);

        return this.Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetTodoListById(long id)
    {
        var todoList = this.todoListService.GetById(id);

        if (todoList.Id == 0)
        {
            return this.BadRequest("Todo List Does Not Exist");
        }

        var todoListDto = new UpdateTodoListDto()
        {
            Id = todoList.Id,
            Name = todoList.Name,
            Description = todoList.Description
        };

        return this.Ok(todoListDto);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTodoList(long id)
    {
        var todoList = this.todoListService.GetById(id);

        if (todoList.Id == 0)
        {
            return this.BadRequest("Todo List Does Not Exist");
        }

        this.todoListService.Delete(id);

        return this.Ok();
    }

    [HttpPut]
    public IActionResult UpdateTodoList(UpdateTodoListDto data)
    {

        var checkTodoList = this.todoListService.GetById(data.Id);

        if (checkTodoList.Id == 0)
        {
            return this.BadRequest("Todo List Does Not Exist");
        }

        var todoList = new TodoList()
        {
            Id = data.Id,
            Name = data.Name,
            Description = data.Description,
            UpdatedDate = DateTime.Now
        };

        this.todoListService.Update(todoList);

        return this.Ok();
    }
}
