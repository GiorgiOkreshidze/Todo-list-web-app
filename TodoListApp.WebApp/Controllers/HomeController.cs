using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.WebApi.Models;
using TodoListApp.WebApp.Models;
using TodoListApp.WebApp.Services;

namespace TodoListApp.WebApp.Controllers;

#pragma warning disable S4487 // Unread "private" fields should be removed
#pragma warning disable IDE0052 // Remove unread private members

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UsersWebApiService authApiService;

    public HomeController(ILogger<HomeController> logger, UsersWebApiService authApiService)
    {
        this._logger = logger;
        this.authApiService = authApiService;
    }

    public IActionResult Index()
    {
        bool isActive = false;

        string token = this.HttpContext.Session.GetString("JWT")!;
        if (token != null)
        {
            this.authApiService.SetBearerToken(token);
            isActive = this.authApiService.ValidateConnection();
        }

        if (isActive)
        {
            return this.RedirectToAction("Index", "TodoTasksList");
        }

        return this.View();
    }


    [ResponseCache(Duration = 1, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(string errorMessage)
    {
        return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier, ErrorMessage = errorMessage });
    }



    public IActionResult LoginUser(UserDto user)
    {
        string token;
        try
        {
            token = this.authApiService.LoginUser(user);
        }
        catch (ApplicationException ex)
        {
            return this.RedirectToAction("Error", new { errorMessage = ex.Message });
        }

        this.HttpContext.Session.SetString("JWT", token);

        return this.RedirectToAction("Index", "TodoTasksList");
    }

    public IActionResult LogOff()
    {
        this.HttpContext.Session.Clear();
        return this.RedirectToAction("Index");
    }


    public IActionResult Register(UserDto user)
    {
        try
        {
            this.authApiService.Register(user);
        }
        catch (ApplicationException ex)
        {
            return this.RedirectToAction("Error", new { errorMessage = ex.Message });
        }

        return this.RedirectToAction("Index");
    }
}
