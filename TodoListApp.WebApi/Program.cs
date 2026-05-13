using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TodoListApp.Services.Database.TodoDatabase;
using TodoListApp.Services.Database.TodoDatabase.Services;
using TodoListApp.Services.Database.UsersDatabase;
using TodoListApp.Services.Database.UsersDatabase.Services;
using TodoListApp.Services.Interfaces.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<TodoListDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TodoListDbConnection")));
builder.Services.AddDbContext<UsersDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UsersDbConnection")));

builder.Services.AddScoped<ITodoListService, TodoListDatabaseService>();
builder.Services.AddScoped<ITodoTaskService, TodoTaskDatabaseService>();
builder.Services.AddScoped<ITodoTaskCommentsService, TodoTaskCommentDatabaseService>();
builder.Services.AddScoped<IUsersService, UsersDatabaseService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
