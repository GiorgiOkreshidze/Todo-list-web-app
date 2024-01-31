namespace TodoListApp.Services.Database;
public class UserRoleEntity
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public IEnumerable<UserEntity> UserEntities { get; set; } = Enumerable.Empty<UserEntity>();
}
