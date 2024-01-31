namespace TodoListApp.Services.Database;
public class UsersDatabaseService : IUsersService
{
    private readonly UsersDbContext context;

    public UsersDatabaseService(UsersDbContext context)
    {
        this.context = context;
    }

    public void Register(User user)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

        var entity = new UserEntity()
        {
            UserName = user.UserName,
            PasswordHash = passwordHash
        };

        _ = this.context.Add(entity);
        _ = this.context.SaveChanges();
    }

    public bool CheckPassword(User user)
    {
        var entity = new UserEntity()
        {
            UserName = user.UserName,
            PasswordHash = user.PasswordHash
        };

        Func<UserEntity, bool> predicate = u =>
            !string.IsNullOrEmpty(entity.PasswordHash) &&
            !string.IsNullOrEmpty(u.PasswordHash) &&
            BCrypt.Net.BCrypt.Verify(entity.PasswordHash, u.PasswordHash);

        return this.context.Set<UserEntity>().Any(predicate);
    }

    public bool CheckUserName(User user)
    {
        var entity = new UserEntity()
        {
            UserName = user.UserName,
        };

        return this.context.Set<UserEntity>().Where(u => u.UserName == entity.UserName).Any();
    }


}




