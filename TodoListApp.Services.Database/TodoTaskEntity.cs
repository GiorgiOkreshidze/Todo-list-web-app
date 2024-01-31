using System.ComponentModel.DataAnnotations;

namespace TodoListApp.Services.Database;
public class TodoTaskEntity
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public DateTime DueDate { get; set; }

    public TodoListEntity TodoListEntity { get; set; } = new TodoListEntity();

    public bool Status { get; set; } = false;

    public string Assignee { get; set; } = string.Empty;

    public ICollection<TodoTaskCommentEntity> TodoTaskCommentEntities { get; set; } = new List<TodoTaskCommentEntity>();

    public ICollection<TagEntity> TagEntities { get; set; } = new List<TagEntity>();


}
