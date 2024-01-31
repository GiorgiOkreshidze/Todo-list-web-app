using Microsoft.AspNetCore.Mvc;
using TodoListApp.WebApi.Models;
using TodoListApp.WebApp.Services;

namespace TodoListApp.WebApp.Controllers;
#pragma warning disable CS8604
public class TodoTaskController : Controller
{
    private readonly TodoTaskWebApiService apiService;

    public TodoTaskController(TodoTaskWebApiService apiService)
    {
        this.apiService = apiService;
    }

    public IActionResult Index(long listId, string sortOrder, string searchString)
    {
        this.ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        this.ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
        this.ViewData["CurrentFilter"] = searchString;
        this.ViewData["ListId"] = listId;

        IEnumerable<TodoTaskDto> todoTaskDtos;

        try
        {
            this.apiService.SetBearerToken(this.HttpContext.Session.GetString("JWT"));

            todoTaskDtos = this.apiService.GetTasksByListId(listId);

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
            todoTaskDtos = todoTaskDtos.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())
                                   || s.Description.ToUpper().Contains(searchString.ToUpper())
                                   || s.DueDate.ToString().Contains(searchString.ToUpper()));
        }
        switch (sortOrder)
        {
            case "name_desc":
                todoTaskDtos = todoTaskDtos.OrderByDescending(t => t.Name);
                break;
            case "Date":
                todoTaskDtos = todoTaskDtos.OrderBy(t => t.DueDate);
                break;
            case "date_desc":
                todoTaskDtos = todoTaskDtos.OrderByDescending(t => t.DueDate);
                break;
            default:
                todoTaskDtos = todoTaskDtos.OrderBy(t => t.Status);
                break;
        }

        return this.View(todoTaskDtos);
    }

    [HttpGet]
    public IActionResult Create(long listId)
    {
        this.ViewData["ListId"] = listId;

        return this.View();
    }

    [HttpPost]
    public IActionResult Create(CreateTodoTaskDto data, long listId)
    {
        try
        {
            this.apiService.SetBearerToken(this.HttpContext.Session.GetString("JWT"));

            this.apiService.CreateTodoTask(data);

        }
        catch (ApplicationException ex)
        {
            return this.RedirectToAction("Error", "Home", new
            {
                errorMessage = ex.Message
            });
        }

        return this.RedirectToAction("Index", new { listId = listId });
    }

    [HttpGet]
    public IActionResult Edit(long id, long listId)
    {
        this.ViewData["ListId"] = listId;

        UpdateTodoTaskDto todoTaskData;

        try
        {
            this.apiService.SetBearerToken(this.HttpContext.Session.GetString("JWT"));

            todoTaskData = this.apiService.GetTodoTaskByIdForUpdate(id);

        }
        catch (ApplicationException ex)
        {
            return this.RedirectToAction("Error", "Home", new
            {
                errorMessage = ex.Message
            });
        }
        return this.View(todoTaskData);
    }

    [HttpPost]
    public IActionResult Edit(UpdateTodoTaskDto data, long listId)
    {
        try
        {
            this.apiService.SetBearerToken(this.HttpContext.Session.GetString("JWT"));

            this.apiService.UpdateTodoTask(data);

        }
        catch (ApplicationException ex)
        {
            return this.RedirectToAction("Error", "Home", new
            {
                errorMessage = ex.Message
            });
        }

        return this.RedirectToAction("Index", new { listId = listId });
    }

    public IActionResult MakeItDone(long id, long listId)
    {
        try
        {
            this.apiService.SetBearerToken(this.HttpContext.Session.GetString("JWT"));

            this.apiService.MakeItDone(id);
        }
        catch (ApplicationException ex)
        {
            return this.RedirectToAction("Error", "Home", new
            {
                errorMessage = ex.Message
            });
        }

        return this.RedirectToAction("Index", new { listId = listId });
    }

    [HttpGet]
    public IActionResult Delete(long id, long listId)
    {
        this.ViewData["ListId"] = listId;

        TodoTaskDto todoTaskData;

        try
        {
            this.apiService.SetBearerToken(this.HttpContext.Session.GetString("JWT"));

            todoTaskData = this.apiService.GetTodoTaskByIdForDelete(id);

        }
        catch (ApplicationException ex)
        {
            return this.RedirectToAction("Error", "Home", new
            {
                errorMessage = ex.Message
            });
        }

        return this.View(todoTaskData);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(long id, long listId)
    {
        try
        {
            this.apiService.SetBearerToken(this.HttpContext.Session.GetString("JWT"));

            this.apiService.Delete(id);
        }
        catch (ApplicationException ex)
        {
            return this.RedirectToAction("Error", "Home", new
            {
                errorMessage = ex.Message
            });
        }

        return this.RedirectToAction("Index", new { listId = listId });
    }

    [HttpGet]
    public IActionResult Details(long id, long listId)
    {
        this.ViewData["ListId"] = listId;

        TodoTaskDto todoTaskDto;

        try
        {
            this.apiService.SetBearerToken(this.HttpContext.Session.GetString("JWT"));
            todoTaskDto = this.apiService.GetTaskById(id);
            this.ViewData["CommentsList"] = this.apiService.GetComments(id);

            this.ViewData["TagsList"] = this.apiService.GetTagsOfTheTask(id);
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



    [HttpGet]
    public IActionResult CreateComment(long taskId, long listId)
    {
        this.ViewData["ListId"] = listId;
        this.ViewData["TaskId"] = taskId;

        return this.View();
    }

    [HttpPost]
    public IActionResult CreateComment(CreateTodoTaskCommentDto data, long taskId, long listId)
    {
        try
        {
            this.apiService.SetBearerToken(this.HttpContext.Session.GetString("JWT"));
            this.apiService.CreateComment(data);
        }
        catch (ApplicationException ex)
        {
            return this.RedirectToAction("Error", "Home", new
            {
                errorMessage = ex.Message
            });
        }

        return this.RedirectToAction("Details", new { id = taskId, listId = listId });
    }

    [HttpGet]
    public IActionResult DeleteComment(long id, long taskId, long listId)
    {
        this.ViewData["ListId"] = listId;
        this.ViewData["TaskId"] = taskId;

        TodoTaskCommentDto todoTaskCommentData;

        try
        {
            this.apiService.SetBearerToken(this.HttpContext.Session.GetString("JWT"));
            todoTaskCommentData = this.apiService.GetTodoTaskCommentByIdForDelete(id);
        }
        catch (ApplicationException ex)
        {
            return this.RedirectToAction("Error", "Home", new
            {
                errorMessage = ex.Message
            });
        }

        return this.View(todoTaskCommentData);
    }

    [HttpPost, ActionName("DeleteComment")]
    public IActionResult DeleteCommentConfirmed(long id, long taskId, long listId)
    {
        try
        {
            this.apiService.SetBearerToken(this.HttpContext.Session.GetString("JWT"));
            this.apiService.DeleteComment(id);
        }
        catch (ApplicationException ex)
        {
            return this.RedirectToAction("Error", "Home", new
            {
                errorMessage = ex.Message
            });
        }

        return this.RedirectToAction("Details", new { id = taskId, listId = listId });
    }

    [HttpGet]
    public IActionResult EditComment(long id, long listId, long taskId)
    {
        this.ViewData["ListId"] = listId;
        this.ViewData["TaskId"] = taskId;

        UpdateTodoTaskCommentDto todoTaskCommentData;

        try
        {
            this.apiService.SetBearerToken(this.HttpContext.Session.GetString("JWT"));
            todoTaskCommentData = this.apiService.GetTodoTaskCommentByIdForUpdate(id);
        }
        catch (ApplicationException ex)
        {
            return this.RedirectToAction("Error", "Home", new
            {
                errorMessage = ex.Message
            });
        }

        return this.View(todoTaskCommentData);
    }

    [HttpPost]
    public IActionResult EditComment(UpdateTodoTaskCommentDto data, long taskId, long listId)
    {

        try
        {
            this.apiService.SetBearerToken(this.HttpContext.Session.GetString("JWT"));
            this.apiService.UpdateTodoTaskComment(data);
        }
        catch (ApplicationException ex)
        {
            return this.RedirectToAction("Error", "Home", new
            {
                errorMessage = ex.Message
            });
        }

        return this.RedirectToAction("Details", new { id = taskId, listId = listId });
    }


    public IActionResult DeleteTag(long tagId, long taskId, long listId)
    {
        try
        {
            this.apiService.SetBearerToken(this.HttpContext.Session.GetString("JWT"));
            this.apiService.DeleteTag(tagId, taskId);
        }
        catch (ApplicationException ex)
        {
            return this.RedirectToAction("Error", "Home", new
            {
                errorMessage = ex.Message
            });
        }

        return this.RedirectToAction("Details", new { id = taskId, listId = listId });
    }

    [HttpGet]
    public IActionResult AddTag(long taskId, long listId)
    {
        this.ViewData["ListId"] = listId;
        this.ViewData["TaskId"] = taskId;

        List<TagsDto> tags;
        List<TagsDto> tagsOfTheTask;

        try
        {
            this.apiService.SetBearerToken(this.HttpContext.Session.GetString("JWT"));
            tags = this.apiService.GetAllTags();
            tagsOfTheTask = this.apiService.GetTagsOfTheTask(taskId);
        }
        catch (ApplicationException ex)
        {
            return this.RedirectToAction("Error", "Home", new
            {
                errorMessage = ex.Message
            });
        }
        tags = tags.Where(tt => !tagsOfTheTask.Exists(t => tt.Id == t.Id)).ToList();

        return this.View(tags);
    }

    [HttpPost]
    public IActionResult AddTag(long tagId, long taskId, long listId)
    {
        this.ViewData["ListId"] = listId;
        this.ViewData["TaskId"] = taskId;

        try
        {
            this.apiService.SetBearerToken(this.HttpContext.Session.GetString("JWT"));
            this.apiService.AddTag(tagId, taskId);
        }
        catch (ApplicationException ex)
        {
            return this.RedirectToAction("Error", "Home", new
            {
                errorMessage = ex.Message
            });
        }


        return this.RedirectToAction("AddTag", new { taskId = taskId, listId = listId });
    }
}
