using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TodoListApp.Services;
using TodoListApp.WebApi.Models;

namespace TodoListApp.WebApi.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IUsersService usersService;
    private readonly IConfiguration _configuration;

    public AuthController(IUsersService usersService, IConfiguration configuration)
    {
        this.usersService = usersService;
        this._configuration = configuration;
    }

    [HttpPost]
    public IActionResult Register(UserDto request)
    {
        var user = new User()
        {
            UserName = request.UserName,
            PasswordHash = request.Password
        };


        if (this.usersService.CheckUserName(user))
        {
            return this.BadRequest("User already registered.");
        }

        this.usersService.Register(user);

        return this.Ok();
    }


    [HttpPost]
    public IActionResult Login(UserDto request)
    {
        var user = new User()
        {
            UserName = request.UserName,
            PasswordHash = request.Password
        };

        if (!this.usersService.CheckUserName(user))
        {
            return this.BadRequest("User not found.");
        }
        if (!this.usersService.CheckPassword(user))
        {
            return this.BadRequest("Wrong password.");
        }

        string token = this.CreateToken(user);

        return this.Ok(token);
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName)
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(this._configuration.GetSection("AppSettings:Token").Value));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: cred
            );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }


    [Authorize]
    [HttpGet]
    public void ValidateConnection()
    {
        // Method intentionally left empty to check if user is logged in.
    }
}
