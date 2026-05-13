using Microsoft.AspNetCore.Mvc;
using TodoListApp.WebApi.Models;
using TodoListApp.Services.WebApi.Interfaces;

namespace TodoListApp.WebApp.Controllers;


public class TodoTasksListController : Controller
{
    private readonly ITodoTaskWebApiService _apiService;

    public TodoTasksListController(ITodoTaskWebApiService apiService)
    {
        this._apiService = apiService;
    }

    public ActionResult Index(long tagId, string sortOrder, string searchString, bool assignedToMe)
    {
        this.ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        this.ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
        this.ViewData["CurrentFilter"] = searchString;
        this.ViewData["TagId"] = tagId;
        this.ViewData["AssignedToMe"] = assignedToMe;

        IEnumerable<TodoTaskDto> filteredTodoTasksDto;

        try
        {
            this._apiService.SetBearerToken(this.HttpContext.Session.GetString("JWT"));

            this.ViewData["TagsList"] = this._apiService.GetAllTags();

            filteredTodoTasksDto = this._apiService.FilterTasksByTagIdOrAssignedToMe(assignedToMe, tagId);
        }
        catch (ApplicationException ex)
        {
            return this.RedirectToAction("Error", "Home", new
            {
                errorMessage = ex.Message
            });
        }

        if (!string.IsNullOrEmpty(searchString))
        {
            filteredTodoTasksDto = filteredTodoTasksDto.Where(s => s.Name.Contains(searchString)
                                   || s.Description.ToUpper().Contains(searchString.ToUpper())
                                   || s.DueDate.ToString().Contains(searchString.ToUpper()));
        }
        switch (sortOrder)
        {
            case "name_desc":
                filteredTodoTasksDto = filteredTodoTasksDto.OrderByDescending(t => t.Name);
                break;
            case "Date":
                filteredTodoTasksDto = filteredTodoTasksDto.OrderBy(t => t.DueDate);
                break;
            case "date_desc":
                filteredTodoTasksDto = filteredTodoTasksDto.OrderByDescending(t => t.DueDate);
                break;
            default:
                filteredTodoTasksDto = filteredTodoTasksDto.OrderBy(t => t.Status);
                break;
        }

        return this.View(filteredTodoTasksDto);
    }

    [HttpGet]
    public IActionResult Details(long id, long listId)
    {
        this.ViewData["ListId"] = listId;

        TodoTaskDto todoTaskDto;

        try
        {
            this._apiService.SetBearerToken(this.HttpContext.Session.GetString("JWT"));

            todoTaskDto = this._apiService.GetTaskById(id);

            this.ViewData["CommentsList"] = this._apiService.GetComments(id);

            this.ViewData["TagsList"] = this._apiService.GetTagsOfTheTask(id);
        }
        catch (ApplicationException ex)
        {
            return this.RedirectToAction("Error", "Home", new
            {
                errorMessage = ex.Message
            });
        }

        return this.View(todoTaskDto);
    }
}
