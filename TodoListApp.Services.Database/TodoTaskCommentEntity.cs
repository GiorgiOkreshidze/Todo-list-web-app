using System.ComponentModel.DataAnnotations;

namespace TodoListApp.Services.Database;
public class TodoTaskCommentEntity
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    public DateTime Created { get; set; }

    public DateTime Updated { get; set; }

    public TodoTaskEntity todoTaskEntity { get; set; } = new TodoTaskEntity();
}
