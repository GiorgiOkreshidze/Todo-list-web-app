using Microsoft.AspNetCore.Mvc;
using TodoListApp.WebApi.Models;
using TodoListApp.Services.WebApi.Interfaces;

namespace TodoListApp.WebApp.Controllers;

public class TodoListController : Controller
{
    private readonly ITodoListWebApiService apiService;

    public TodoListController(ITodoListWebApiService apiService)
    {
        this.apiService = apiService;
    }

    public IActionResult Index()
    {
        List<TodoListDto> todoListDtos;
        try
        {

            this.apiService.SetBearerToken(this.HttpContext.Session.GetString("JWT"));
            todoListDtos = this.apiService.GetTodoLists();

        }
        catch (ApplicationException ex)
        {
            return this.RedirectToAction("Error", "Home", new
            {
                errorMessage = ex.Message
            });
        }

        return this.View(todoListDtos);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return this.View();
    }

    [HttpPost]
    public IActionResult Create(TodoListDto data)
    {
        try
        {
            this.apiService.SetBearerToken(this.HttpContext.Session.GetString("JWT"));
            this.apiService.CreateTodoList(data);

        }
        catch (ApplicationException ex)
        {
            return this.RedirectToAction("Error", "Home", new
            {
                errorMessage = ex.Message
            });
        }

        return this.RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(long id)
    {
        UpdateTodoListDto todoListData;
        try
        {
            this.apiService.SetBearerToken(this.HttpContext.Session.GetString("JWT"));
            todoListData = this.apiService.GetTodoListByIdForUpdate(id);

        }
        catch (ApplicationException ex)
        {
            return this.RedirectToAction("Error", "Home", new
            {
                errorMessage = ex.Message
            });
        }

        return this.View(todoListData);
    }

    [HttpPost]
    public IActionResult Edit(UpdateTodoListDto data)
    {
        try
        {
            this.apiService.SetBearerToken(this.HttpContext.Session.GetString("JWT"));
            this.apiService.UpdateTodoList(data);

        }
        catch (ApplicationException ex)
        {
            return this.RedirectToAction("Error", "Home", new
            {
                errorMessage = ex.Message
            });
        }

        return this.RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(long id)
    {
        TodoListDto todoListData;
        try
        {
            this.apiService.SetBearerToken(this.HttpContext.Session.GetString("JWT"));
            todoListData = this.apiService.GetTodoListByIdForDelete(id);

        }
        catch (ApplicationException ex)
        {
            return this.RedirectToAction("Error", "Home", new
            {
                errorMessage = ex.Message
            });
        }
        return this.View(todoListData);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(long id)
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
        return this.RedirectToAction("Index");
    }
}
