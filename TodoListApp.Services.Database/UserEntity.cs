using System.ComponentModel.DataAnnotations;

namespace TodoListApp.Services.Database;
public class UserEntity
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string UserName { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    public byte[] PasswordHashed { get; set; } = new byte[0];

    public IEnumerable<UserRoleEntity> UserRoleEntities { get; set; } = Enumerable.Empty<UserRoleEntity>();
}
