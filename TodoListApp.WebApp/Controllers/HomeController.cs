using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.WebApi.Models;
using TodoListApp.WebApp.Models;
using TodoListApp.Services.WebApi.Interfaces;

namespace TodoListApp.WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUsersWebApiService _authApiService;

    public HomeController(ILogger<HomeController> logger, IUsersWebApiService authApiService)
    {
        this._logger = logger;
        this._authApiService = authApiService;
    }

    public IActionResult Index()
    {
        bool isActive = false;
        if (this.HttpContext.Session.GetString("JWT") is { } token)
        {
            this._authApiService.SetBearerToken(token);
            isActive = this._authApiService.ValidateConnection();
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
        return this.View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier,
            ErrorMessage = errorMessage
        });
    }



    public IActionResult LoginUser(UserDto user)
    {
        string token;
        try
        {
            token = this._authApiService.LoginUser(user);
        }
        catch (ApplicationException ex)
        {
            this._logger.LogError(ex.Message, "Failed to login");
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
            this._authApiService.Register(user);
        }
        catch (ApplicationException ex)
        {
            this._logger.LogError(ex.Message, "Failed to Register");
            return this.RedirectToAction("Error", new { errorMessage = ex.Message });
        }

        return this.RedirectToAction("Index");
    }
}
